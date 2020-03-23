package net.verdagon.chronobasecs.parser

import net.verdagon.chronobasecs._
import net.verdagon.chronobasecs.parsed._

import scala.util.parsing.combinator.RegexParsers

object VCSParser extends RegexParsers {
  override def skipWhitespace = false
  override val whiteSpace = "[ \t\r\f]+".r

  def white: Parser[Unit] = { "\\s+".r ^^^ Unit }
  def optWhite: Parser[Unit] = { opt(white) ^^^ Unit }

  // soon, give special treatment to ^
  // we want marine^.item to explode marine and extract its item
  // but, we don't want it to parse it as (marine^).item
  // so, we need to look ahead a bit and see if there's a . after it.
  def exprIdentifier: Parser[String] = {
    """[^\s\.\$\&\,\:\(\)\;\[\]\{\}\'\^\"]+""".r
  }

  def exprIdentifierWithDots: Parser[String] = {
    """[^\s\$\&\,\:\(\)\;\[\]\{\}\'\^\"]+""".r
  }

  def typeIdentifier: Parser[String] = {
    """[^\s\.\!\?\#\$\&\,\:\|\;\(\)\[\]\{\}=]+""".r
  }

  def externIdentifier: Parser[String] = {
    """[^\s\!\?\#\$\&\,\:\|\;\(\)\[\]\{\}=]+""".r
  }

  def stringOr[T](string: String, parser: Parser[T]): Parser[Option[T]] = {
    (string ^^^ { val x: Option[T] = None; x } | parser ^^ (a => Some(a)))
  }

  def underscoreOr[T](parser: Parser[T]): Parser[Option[T]] = {
    ("_" ^^^ { val x: Option[T] = None; x } | parser ^^ (a => Some(a)))
  }

  def exists[T](parser: Parser[T]): Parser[Boolean] = {
    opt(parser) ^^ (_.nonEmpty)
  }

  def rune: Parser[String] = {
    "#" ~> optWhite ~> typeIdentifier
  }

  def int: Parser[Int] = {
    raw"^-?\d+".r ^^ {
      case thingStr => thingStr.toInt
    }
  }

  def string: Parser[String] = {
    "\"" ~> "[^\"]*".r <~ "\""
  }

  // ww = with whitespace

  sealed trait ITopLevelThing
  case class StructTLT(struct: StructP) extends ITopLevelThing
  case class InterfaceTLT(interface: InterfaceP) extends ITopLevelThing
  case class ImplTLT(impl: ImplP) extends ITopLevelThing
  case class FunctionTLT(function: FunctionP) extends ITopLevelThing

  def tyype: Parser[TypeP] = {
    exists("?" <~ optWhite) ~
    exists("&&" <~ optWhite) ~
    exists("&" <~ optWhite) ~
      (((typeIdentifier <~ optWhite <~ ":" <~ optWhite <~ "(" <~ optWhite) ~ (repsep(tyype, optWhite ~ "," ~ optWhite) <~ optWhite <~ ")") ^^ {
        case template ~ args => TemplateKindP(template, args)
      }) |
      ((typeIdentifier <~ optWhite <~ ":" <~ optWhite) ~ tyype ^^ {
        case template ~ arg => TemplateKindP(template, List(arg))
      }) |
      ("Void" ^^^ VoidKindP) |
      ("Int" ^^^ IntKindP) |
      ("Float" ^^^ FloatKindP) |
      ("Str" ^^^ StrKindP) |
      ("Bool" ^^^ BoolKindP) |
      ("$" ~> externIdentifier ^^ ExternKindP) |
      (typeIdentifier ^^ NameKindP)) ^^ {
      case nullable ~ weak ~ strong ~ kind => {
        val ownership = if (weak) { WeakP } else if (strong) StrongP else OwnP
        TypeP(nullable, ownership, kind)
      }
    }
  }

  def structMember: Parser[StructMemberP] = {
    exists("var" <~ optWhite) ~
      (typeIdentifier <~ optWhite <~ ":" <~ optWhite) ~
      (tyype <~ optWhite <~ ";") ^^ {
      case varying ~ memberName ~ memberType => {
        StructMemberP(
          memberName,
          if (varying) VaryingP else FinalP,
          memberType)
      }
    }
  }

  def struct: Parser[StructP] = {
    exists("root" <~ white) ~
    (("imm" | "mut") <~ white) ~
    ( "struct" ~> white ~> typeIdentifier <~ optWhite <~ "{" <~ optWhite) ~
    (repsep(impl, optWhite) <~ optWhite) ~
    (repsep(structMember, optWhite) <~ optWhite) ~
    (repsep(function, optWhite) <~ optWhite) <~ optWhite <~ "}" ^^ {
      case isRoot ~ mutability ~ name ~ impls ~ members ~ functions => {
        StructP(
          name,
          isRoot,
          if (mutability == "imm") ImmutableP else MutableP,
          members,
          impls,
          functions)
      }
    }
  }

  def parameter: Parser[ParameterP] = {
    (typeIdentifier <~ optWhite <~ ":" <~ optWhite) ~
    tyype ~
    opt(optWhite ~> "overrides" ~> optWhite ~> typeIdentifier) ^^ {
      case name ~ tyype ~ maybeOverride => {
        ParameterP(name, tyype, maybeOverride)
      }
    }
  }

  def signature: Parser[SignatureP] = {
    ("fn" ~> white ~> exprIdentifier <~ optWhite <~ "(" <~ optWhite) ~
      repsep(parameter, optWhite ~> "," <~ optWhite) ~
      (optWhite ~> ")" ~> optWhite ~> ":" ~> optWhite ~> tyype) ^^ {
      case name ~ parameters ~ returnType => {
        SignatureP(name, returnType, parameters)
      }
    }
  }

  def interfaceMember: Parser[SignatureP] = {
    signature <~ ";"
  }

  def interface: Parser[InterfaceP] = {
      (("imm" | "mut") <~ white) ~
      ( "interface" ~> white ~> typeIdentifier <~ optWhite <~ "{" <~ optWhite) ~
      (repsep(impl, optWhite) <~ optWhite) ~
      repsep(interfaceMember, optWhite) <~ optWhite <~ "}" ^^ {

      case mutability ~ name ~ impls ~ members => {
        InterfaceP(
          name,
          if (mutability == "imm") ImmutableP else MutableP,
          impls,
          members)
      }
    }
  }

  def impl: Parser[ImplP] = {
    (typeIdentifier <~ white <~ "isa" <~ white) ~ typeIdentifier <~ optWhite <~ ";" ^^ {
      case struct ~ interface => {
        ImplP(struct, interface)
      }
    }
  }

  def function: Parser[FunctionP] = {
    (signature <~ optWhite <~ "{" <~ optWhite) ~
      (exprIdentifierWithDots <~ optWhite <~ "}") ^^ {
      case signature ~ externalFunctionName => {
        FunctionP(signature, externalFunctionName)
      }
    }
  }

  def topLevelThing: Parser[ITopLevelThing] = {
    struct ^^ StructTLT |
    interface ^^ InterfaceTLT |
    impl ^^ ImplTLT |
    function ^^ FunctionTLT
  }

  def superstructure: Parser[SuperstructureP] = {
    optWhite ~> repsep(topLevelThing, optWhite) <~ optWhite ^^ { case topLevelThings =>
      val structs = topLevelThings.collect({ case StructTLT(s) => s })
      val interfaces = topLevelThings.collect({ case InterfaceTLT(i) => i })
      val impls = topLevelThings.collect({ case ImplTLT(i) => i })
      val functions = topLevelThings.collect({ case FunctionTLT(f) => f })
      SuperstructureP(structs, interfaces, impls, functions)
    }
  }
}
