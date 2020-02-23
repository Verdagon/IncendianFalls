package net.verdagon.chronobasecs.generator.chronobase

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.{ParseSource, Primitives}
import net.verdagon.chronobasecs.generator.chronobase.interface.InterfaceGenerator
import net.verdagon.chronobasecs.generator.chronobase.list.ListGenerator
import net.verdagon.chronobasecs.generator.chronobase.map.MapGenerator
import net.verdagon.chronobasecs.generator.chronobase.set.SetGenerator
import net.verdagon.chronobasecs.generator.chronobase.struct.StructGenerator

case class ChronobaseOptions(hash: Boolean)

object Generator {
  def toCS(variabilityS: VariabilityS): String = {
    variabilityS match {
      case VaryingS => ""
      case FinalS => "readonly"
    }
  }

  def toCS(tyype: TypeS[IKindS]): String = {
    toCS(tyype.kind)
  }

  def toCS(tyype: IKindS): String = {
    tyype match {
      case VoidKindS => "Void"
      case IntKindS => "int"
      case BoolKindS => "bool"
      case StrKindS => "string"
      case FloatKindS => "float"
      case ExternKindS(name) => name
      case StructKindS(name, _) => name
      case InterfaceKindS(name, _) => name
      case ListKindS(name, _) => name
      case SetKindS(name, _) => name
      case MapKindS(name, _) => name
    }
  }

  def signatureToString(signature: SignatureS): String = {
    val SignatureS(methodName, returnType, parameters) = signature;
    toCS(returnType) + s" ${methodName}(" +
      parameters.map({ case ParameterS(paramName, paramType, _) =>
        toCS(paramType) + " " + paramName
      }).mkString(", ") +
      s")"
  }

  def generateSuperstructure(opt: ChronobaseOptions, ss: SuperstructureS): Map[String, String] = {
    Map[String, String](
      "Root" -> Root.generateRoot(opt, ss),
      "RootIncarnation" -> RootIncarnation.generateRootIncarnation(opt, ss),
      "PrimitivesExtensions" -> Primitives.definition) ++
      ParseSource.files ++
      ss.structs.flatMap(s => StructGenerator.generateHandles(opt, ss, s)).toMap ++
      ss.interfaces.flatMap(s => InterfaceGenerator.generateHandles(opt, ss, s)).toMap ++
      ss.lists.flatMap(s => ListGenerator.generateHandles(opt, s)).toMap ++
      ss.sets.flatMap(s => SetGenerator.generateHandles(opt, s)).toMap ++
      ss.maps.flatMap(s => MapGenerator.generateHandles(opt, s)).toMap
  }
}
