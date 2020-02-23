package net.verdagon.chronobasecs.parsed

sealed trait MutabilityP
case object MutableP extends MutabilityP
case object ImmutableP extends MutabilityP

sealed trait VariabilityP
case object FinalP extends VariabilityP
case object VaryingP extends VariabilityP

case class SuperstructureP(
    structs: List[StructP],
    interfaces: List[InterfaceP],
    topLevelImpls: List[ImplP],
    topLevelFunctions: List[FunctionP]) {
  def hasInterface(name: String): Boolean = {
    interfaces.exists(_.name == name)
  }
  def hasStruct(name: String): Boolean = {
    structs.exists(_.name == name)
  }
  def getInterface(name: String): InterfaceP = {
    interfaces.find(_.name == name) match {
      case None => throw new RuntimeException("Can't find interface " + name)
      case Some(s) => s
    }
  }
  def getStruct(name: String): StructP = {
    structs.find(_.name == name) match {
      case None => throw new RuntimeException("Can't find struct " + name)
      case Some(s) => s
    }
  }
  def impls: List[ImplP] = {
    topLevelImpls ++ interfaces.flatMap(_.impls) ++ structs.flatMap(_.impls)
  }
  def functions: List[FunctionP] = {
    topLevelFunctions ++ structs.flatMap(_.functions)
  }
  def getImplementingStructs(interface: String): List[String] = {
    impls
      .filter(_.interface == interface)
      .flatMap({ case ImplP(sub, _) =>
        if (interfaces.exists(_.name == sub)) {
          getImplementingStructs(sub)
        } else if (structs.exists(_.name == sub)) {
          List(sub)
        } else {
          throw new RuntimeException("wat")
        }
      })
      .distinct
  }
  def getAncestorInterfaces(sub: String, includeSelf: Boolean): List[String] = {
    (if (includeSelf) List(sub) else List()) ++
    impls
      .filter(_.sub == sub)
      .flatMap({ case ImplP(_, parent) =>
        getAncestorInterfaces(parent, true)
      })
      .distinct
  }
  def getImplementingInterfaces(suuper: String): List[String] = {
    println("impling interfaces for " + suuper + ": " + impls
      .filter(_.interface == suuper)
      .map(_.sub))
    impls
      .filter(_.interface == suuper)
      .map(_.sub)
      .filter(hasInterface)
      .flatMap(sub => List(sub) ++ getImplementingInterfaces(sub))
      .distinct
  }
}

case class InterfaceP(
  name: String,
  mutability: MutabilityP,
  impls: List[ImplP],
  methods: List[SignatureP])

case class SignatureP(
  name: String,
  returnType: TypeP,
  parameters: List[ParameterP]) {

  def maybeOverriddenInterface: Option[String] = {
    parameters.flatMap(_.maybeOverride).headOption
  }
}

case class ImplP(
  sub: String,
  interface: String) {

  def typeclassName = sub + "As" + interface
}

case class FunctionP(
    signature: SignatureP,
    externalFunctionName: String) {
  def maybeOverriddenInterface: Option[String] = signature.maybeOverriddenInterface
}

case class ParameterP(
  name: String,
  tyype: TypeP,
  maybeOverride: Option[String])

case class StructP(
    name: String,
    isRoot: Boolean,
    mutability: MutabilityP,
    members: List[StructMemberP],
    impls: List[ImplP],
    functions: List[FunctionP]) {
  def incarnationName = name + "Incarnation"
}

case class StructMemberP(
  name: String,
  variability: VariabilityP,
  tyype: TypeP)

sealed trait IOwnershipP
case object OwnP extends IOwnershipP
case object StrongP extends IOwnershipP
case object WeakP extends IOwnershipP

case class TypeP(nullable: Boolean, ownership: IOwnershipP, kind: IKindP)

sealed trait IKindP
case object VoidKindP extends IKindP
case object IntKindP extends IKindP
case object BoolKindP extends IKindP
case object StrKindP extends IKindP
case object FloatKindP extends IKindP
case class ExternKindP(name: String) extends IKindP
case class NameKindP(name: String) extends IKindP
case class TemplateKindP(template: String, args: List[TypeP]) extends IKindP