package net.verdagon.chronobasecs.compiled

sealed trait MutabilityS
case object MutableS extends MutabilityS
case object ImmutableS extends MutabilityS

sealed trait VariabilityS
case object FinalS extends VariabilityS
case object VaryingS extends VariabilityS

case class SuperstructureS(
    lists: List[ListS],
    sets: List[SetS],
    maps: List[MapS],
    bunches: List[BunchS],
    structs: List[StructS],
    interfaces: List[InterfaceS],
    functions: List[FunctionS]) {

  def addStruct(structS: StructS): SuperstructureS = {
    structs.find(_.name == structS.name) match {
      case None => {
        SuperstructureS(lists, sets, maps, bunches, structS :: structs, interfaces, functions)
      }
      case Some(existing) => {
        if (structS != existing) {
          throw new RuntimeException("Tried to add, but already existed in incompatible form!\n" + structS + "\n" + existing)
        }
        this
      }
    }
  }

  def addInterface(interfaceS: InterfaceS): SuperstructureS = {
    interfaces.find(_.name == interfaceS.name) match {
      case None => {
        SuperstructureS(lists, sets, maps, bunches, structs, interfaceS :: interfaces, functions)
      }
      case Some(existing) => {
        if (interfaceS != existing) {
          throw new RuntimeException("Tried to add, but already existed in incompatible form!\n" + interfaceS + "\n" + existing)
        }
        this
      }
    }
  }

  def addFunction(functionS: FunctionS): SuperstructureS = {
    functions.find(_.signature == functionS.signature) match {
      case None => {
        SuperstructureS(lists, sets, maps, bunches, structs, interfaces, functionS :: functions)
      }
      case Some(existing) => {
        if (functionS != existing) {
          throw new RuntimeException("Tried to add, but already existed in incompatible form!\n" + functionS + "\n" + existing)
        }
        this
      }
    }
  }

  def addList(list: ListS): SuperstructureS = {
    lists.find(_.name == list.name) match {
      case None => {
        SuperstructureS(lists :+ list, sets, maps, bunches, structs, interfaces, functions)
      }
      case Some(existing) => {
        if (list != existing) {
          throw new RuntimeException("Tried to add, but already existed in incompatible form!\n" + list + "\n" + existing)
        }
        this
      }
    }
  }

  def addSet(set: SetS): SuperstructureS = {
    sets.find(_.name == set.name) match {
      case None => {
        SuperstructureS(lists, sets :+ set, maps, bunches, structs, interfaces, functions)
      }
      case Some(existing) => {
        if (set != existing) {
          throw new RuntimeException("Tried to add, but already existed in incompatible form!\n" + set + "\n" + existing)
        }
        this
      }
    }
  }

  def addMap(map: MapS): SuperstructureS = {
    maps.find(_.name == map.name) match {
      case None => {
        SuperstructureS(lists, sets, maps :+ map, bunches, structs, interfaces, functions)
      }
      case Some(existing) => {
        if (map != existing) {
          throw new RuntimeException("Tried to add, but already existed in incompatible form!\n" + map + "\n" + existing)
        }
        this
      }
    }
  }

  def addBunch(bunch: BunchS): SuperstructureS = {
    bunches.find(_.struct.name == bunch.struct.name) match {
      case None => {
        SuperstructureS(lists, sets, maps, bunches :+ bunch, structs, interfaces, functions)
      }
      case Some(existing) => {
        if (bunch != existing) {
          throw new RuntimeException("Tried to add, but already existed in incompatible form!\n" + bunch + "\n" + existing)
        }
        this
      }
    }
  }

  def hasInterface(name: String): Boolean = {
    interfaces.exists(_.name == name)
  }
  def getInterface(name: String): InterfaceS = {
    interfaces.find(_.name == name) match {
      case None => throw new RuntimeException("Can't find interface " + name)
      case Some(s) => s
    }
  }
  def getStruct(name: String): StructS = {
    structs.find(_.name == name) match {
      case None => throw new RuntimeException("Can't find struct " + name)
      case Some(s) => s
    }
  }
}

case class ListS(
  name: String,
  mutability: MutabilityS,
  element: TypeS[IKindS]) {
  def tyype: ListKindS = {
    ListKindS(name, mutability)
  }
}

case class MapS(
  name: String,
  mutability: MutabilityS,
  key: TypeS[IKindS],
  value: TypeS[IKindS]) {
  def tyype: MapKindS = {
    MapKindS(name, mutability)
  }
}

case class SetS(
  name: String,
  mutability: MutabilityS,
  element: TypeS[IKindS]) {
  def tyype: SetKindS = {
    SetKindS(name, mutability)
  }
}

// A bunch is simply an extension of an existing struct, which
// was generated in the compiling stage. The generator will watch
// out for this and when it makes the struct it will bring in this
// functionality.
case class BunchS(
  struct: StructKindS,
  mutability: MutabilityS,
  interface: InterfaceKindS,
  structs: List[StructKindS],
  structSetByStruct: Map[StructKindS, SetKindS],
  structByInterface: Map[InterfaceKindS, List[StructKindS]])
// Later on, we might add more "extension" things beside bunch.
// Long term, these will be replaced by superstructure functions.

case class InterfaceS(
    name: String,
    mutability: MutabilityS,
    methods: List[SignatureS],
    parentInterfaces: List[InterfaceKindS],
    ancestorInterfaces: List[InterfaceKindS],
    childInterfaces: List[InterfaceKindS],
    childStructs: List[StructKindS],
    descendantInterfaces: List[InterfaceKindS],
    descendantStructs: List[StructKindS]) {
  def tyype: InterfaceKindS = {
    InterfaceKindS(name, mutability)
  }
}

case class SignatureS(
    name: String,
    returnType: TypeS[IKindS],
    parameters: List[ParameterS]) {
  def maybeOverriddenInterface: Option[String] = {
    parameters.flatMap(_.maybeOverride).headOption
  }
}

case class FunctionS(
    signature: SignatureS,
    externalFunctionName: String) {
  def maybeOverriddenInterface: Option[String] = signature.maybeOverriddenInterface
}

case class ParameterS(
  name: String,
  tyype: TypeS[IKindS],
  maybeOverride: Option[String])

case class ImplS(
  interface: InterfaceKindS,
  methods: List[SignatureS])

case class StructS(
    name: String,
    isRoot: Boolean,
    mutability: MutabilityS,
    members: List[StructMemberS],
    impls: List[ImplS],
    parentInterfaces: List[InterfaceKindS],
    ancestorInterfaces: List[InterfaceKindS]) {
  def incarnationName = name + "Incarnation"
  def tyype: StructKindS = {
    StructKindS(name, mutability)
  }
}

case class StructMemberS(
  name: String,
  variability: VariabilityS,
  tyype: TypeS[IKindS])

sealed trait IOwnershipS
case object OwnS extends IOwnershipS
case object StrongS extends IOwnershipS
case object WeakS extends IOwnershipS
case object ShareS extends IOwnershipS

case class TypeS[+T <: IKindS](nullable: Boolean, ownership: IOwnershipS, kind: T) {
  def name = kind.name
  def flatten: TypeS[IKindS] = {
    TypeS(nullable, ownership, kind.flatten)
  }
}

sealed trait IKindS {
  def name: String
  def mutability: MutabilityS
  def flatten: IKindS
  def isPrimitive: Boolean
}
case object VoidKindS extends IKindS {
  override def mutability: MutabilityS = ImmutableS
  override def name = "Void"
  override def flatten: IKindS = this
  override def isPrimitive: Boolean = true
}
case object IntKindS extends IKindS {
  override def mutability: MutabilityS = ImmutableS
  override def name = "Int"
  override def flatten: IKindS = this
  override def isPrimitive: Boolean = true
}
case object BoolKindS extends IKindS {
  override def mutability: MutabilityS = ImmutableS
  override def name = "Bool"
  override def flatten: IKindS = this
  override def isPrimitive: Boolean = true
}
case object StrKindS extends IKindS {
  override def mutability: MutabilityS = ImmutableS
  override def name = "Str"
  override def flatten: IKindS = this
  override def isPrimitive: Boolean = true
}
case object FloatKindS extends IKindS {
  override def mutability: MutabilityS = ImmutableS
  override def name = "Float"
  override def flatten: IKindS = this
  override def isPrimitive: Boolean = true
}
case class ExternKindS(name: String) extends IKindS {
  override def mutability: MutabilityS = ImmutableS
  override def flatten: IKindS = this
  override def isPrimitive: Boolean = false
}
case class StructKindS(name: String, mutability: MutabilityS) extends IKindS {
  override def flatten: IKindS = {
    mutability match {
      case MutableS => IntKindS
      case ImmutableS => this
    }
  }
  override def isPrimitive: Boolean = false
}
case class InterfaceKindS(name: String, mutability: MutabilityS) extends IKindS {
  override def flatten: IKindS = {
    mutability match {
      case MutableS => IntKindS
      case ImmutableS => this
    }
  }
  override def isPrimitive: Boolean = false
}
case class ListKindS(name: String, mutability: MutabilityS) extends IKindS {
  override def flatten: IKindS = {
    mutability match {
      case MutableS => IntKindS
      case ImmutableS => this
    }
  }
  override def isPrimitive: Boolean = false
}
case class SetKindS(name: String, mutability: MutabilityS) extends IKindS {
  override def flatten: IKindS = {
    mutability match {
      case MutableS => IntKindS
      case ImmutableS => this
    }
  }
  override def isPrimitive: Boolean = false
}
case class MapKindS(name: String, mutability: MutabilityS) extends IKindS {
  override def flatten: IKindS = {
    mutability match {
      case MutableS => IntKindS
      case ImmutableS => this
    }
  }
  override def isPrimitive: Boolean = false
}