package net.verdagon.chronobasecs

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.parsed._

object Compiler {
  def compile(ssP: SuperstructureP): SuperstructureS = {
    val SuperstructureP(structs, interfaces, _, _) = ssP

    ssP.impls.foreach(impl => {
      if (!structs.exists(_.name == impl.sub) && !interfaces.exists(_.name == impl.sub)) {
        throw new RuntimeException("Can't find " + impl.sub);
      }
      if (!interfaces.exists(_.name == impl.interface)) {
        throw new RuntimeException("Can't find " + impl.interface);
      }
    })

    val ssS0 =
      SuperstructureS(
        List(),
        List(),
        List(),
        List(),
        List(),
        List(),
        List())

    val ssS2 =
      interfaces.foldLeft(ssS0)({
        case (ssS1, interface) => compileInterface(ssP, ssS1, interface)
      })

    val ssS4 =
      structs.foldLeft(ssS2)({
        case (ssS3, struct) => compileStruct(ssP, ssS3, struct)
      })

    val ssS6 =
      ssP.functions.foldLeft(ssS4)({
        case (ssS5, function) => compileFunction(ssP, ssS5, function)
      })

    println(ssS6)

    ssS6
  }

  def compileMutability(p: MutabilityP): MutabilityS = {
    p match {
      case MutableP => MutableS
      case ImmutableP => ImmutableS
    }
  }

  def compileVariability(v: VariabilityP): VariabilityS = {
    v match {
      case FinalP => FinalS
      case VaryingP => VaryingS
    }
  }

  def compileOwnership(mutability: MutabilityS, o: IOwnershipP): IOwnershipS = {
    mutability match {
      case MutableS => {
        o match {
          case OwnP => OwnS
          case StrongP => StrongS
          case WeakP => WeakS
        }
      }
      case ImmutableS => ShareS
    }
  }

  def uncompileOwnership(o: IOwnershipS): IOwnershipP = {
    o match {
      case OwnS => OwnP
      case StrongS => StrongP
      case WeakS => WeakP
    }
  }

  def compileKinds(ssP: SuperstructureP, ssS0: SuperstructureS, kindsP: List[IKindP]):
  (SuperstructureS, List[IKindS]) = {
    kindsP.foldLeft(ssS0, List[IKindS]())({
      case ((ssS2, previousInterfacesS), kindP) => {
        val (ssS3, interfaceS) = compileKind(ssP, ssS2, kindP)
        (ssS3, interfaceS :: previousInterfacesS)
      }
    })
  }

  def compileTypes(ssP: SuperstructureP, ssS0: SuperstructureS, typesP: List[TypeP]):
  (SuperstructureS, List[TypeS[IKindS]]) = {
    typesP.foldLeft(ssS0, List[TypeS[IKindS]]())({
      case ((ssS2, previousInterfacesS), typeP) => {
        val (ssS3, interfaceS) = compileType(ssP, ssS2, typeP)
        (ssS3, interfaceS :: previousInterfacesS)
      }
    })
  }

  def compileKind(ssP: SuperstructureP, ssS0: SuperstructureS, kindP: IKindP):
  (SuperstructureS, IKindS) = {
    kindP match {
      case VoidKindP => (ssS0, VoidKindS)
      case IntKindP => (ssS0, IntKindS)
      case BoolKindP => (ssS0, BoolKindS)
      case StrKindP => (ssS0, StrKindS)
      case FloatKindP => (ssS0, FloatKindS)
      case ExternKindP(name) => (ssS0, ExternKindS(name))
      case NameKindP(name) => {
        ssP.interfaces.find(_.name == name) match {
          case Some(InterfaceP(_, ImmutableP, _, _)) => (ssS0, InterfaceKindS(name, ImmutableS))
          case Some(InterfaceP(_, MutableP, _, _)) => (ssS0, InterfaceKindS(name, MutableS))
          case None => {
            ssP.structs.find(_.name == name) match {
              case Some(StructP(_, _, ImmutableP, _, _, _)) => (ssS0, StructKindS(name, ImmutableS))
              case Some(StructP(_, _, MutableP, _, _, _)) => (ssS0, StructKindS(name, MutableS))
              case None => throw new RuntimeException("Couldn't find type: " + name)
            }
          }
        }
      }
      case TemplateKindP("ImmList", List(elementTypeP)) => {
        compileList(ssP, ssS0, ImmutableS, elementTypeP)
      }
      case TemplateKindP("MutList", List(elementTypeP)) => {
        compileList(ssP, ssS0, MutableS, elementTypeP)
      }
      case TemplateKindP("ImmSet", List(elementTypeP)) => {
        compileSet(ssP, ssS0, ImmutableS, elementTypeP)
      }
      case TemplateKindP("MutSet", List(elementTypeP)) => {
        compileSet(ssP, ssS0, MutableS, elementTypeP)
      }
      case TemplateKindP("ImmMap", List(keyTypeP, valueTypeP)) => {
        compileMap(ssP, ssS0, ImmutableS, keyTypeP, valueTypeP)
      }
      case TemplateKindP("MutMap", List(keyTypeP, valueTypeP)) => {
        compileMap(ssP, ssS0, MutableS, keyTypeP, valueTypeP)
      }
      case TemplateKindP("ImmBunch", List(elementTypeP)) => {
        compileBunch(ssP, ssS0, ImmutableS, elementTypeP)
      }
      case TemplateKindP("MutBunch", List(elementTypeP)) => {
        compileBunch(ssP, ssS0, MutableS, elementTypeP)
      }
    }
  }

  def compileType(ssP: SuperstructureP, ssS0: SuperstructureS, typeP: TypeP):
  (SuperstructureS, TypeS[IKindS]) = {
    val (ssS1, kindS) = compileKind(ssP, ssS0, typeP.kind)
    (ssS1, TypeS(typeP.nullable, compileOwnership(kindS.mutability, typeP.ownership), kindS))
  }

  def compileList(ssP: SuperstructureP, ssS0: SuperstructureS, mutabilityS: MutabilityS, elementTypeP: TypeP):
  (SuperstructureS, ListKindS) = {
    val (ssS1, elementTypeS) = compileType(ssP, ssS0, elementTypeP)
    val name =
      elementTypeS.name +
        (elementTypeS.ownership match { case OwnS => "" case StrongS => "Strong" case WeakS => "Weak" case ShareS => "" }) +
        (if (elementTypeS.nullable) "Maybe" else "") +
        (if (mutabilityS == MutableS) "Mut" else "Imm") +
        "List"
    val ssS2 = ssS1.addList(ListS(name, mutabilityS, elementTypeS))
    (ssS2, ListKindS(name, mutabilityS))
  }

  def compileSet(ssP: SuperstructureP, ssS0: SuperstructureS, mutabilityS: MutabilityS, elementTypeP: TypeP):
  (SuperstructureS, SetKindS) = {
    val (ssS1, elementTypeS) = compileType(ssP, ssS0, elementTypeP)
    val name =
      elementTypeS.name +
        (elementTypeS.ownership match { case OwnS => "" case StrongS => "Strong" case WeakS => "Weak" case ShareS => "" }) +
        (if (elementTypeS.nullable) "Maybe" else "") +
        (if (mutabilityS == MutableS) "Mut" else "Imm") +
        "Set"
    val ssS2 = ssS1.addSet(SetS(name, mutabilityS, elementTypeS))
    (ssS2, SetKindS(name, mutabilityS))
  }

  def compileMap(ssP: SuperstructureP, ssS0: SuperstructureS, mutabilityS: MutabilityS, keyTypeP: TypeP, elementTypeP: TypeP):
  (SuperstructureS, MapKindS) = {
    val (ssS1, keyTypeS) = compileType(ssP, ssS0, keyTypeP)
    val (ssS2, elementTypeS) = compileType(ssP, ssS1, elementTypeP)
    val name =
      elementTypeS.name +
        (elementTypeS.ownership match { case OwnS => "" case StrongS => "Strong" case WeakS => "Weak" case ShareS => "" }) +
        (if (elementTypeS.nullable) "Maybe" else "") +
        "By" +
        (if (keyTypeS.nullable) "Maybe" else "") + keyTypeS.name + (if (elementTypeS.nullable) "Maybe" else "") + (if (mutabilityS == MutableS) "Mut" else "Imm") + "Map"
    val ssS3 = ssS2.addMap(MapS(name, mutabilityS, keyTypeS, elementTypeS))
    (ssS3, MapKindS(name, mutabilityS))
  }

  def compileBunch(ssP: SuperstructureP, ssS0: SuperstructureS, mutabilityS: MutabilityS, elementTypeP: TypeP):
  (SuperstructureS, StructKindS) = {
    val (ssS1, elementTypeS) = compileType(ssP, ssS0, elementTypeP)
    val interface =
      elementTypeS.kind match {
        case i @ InterfaceKindS(_, _) => i
        case _ => throw new RuntimeException("Bunches can only contain interfaces!")
      }

    val descendantInterfacesP =
        getDescendantInterfaces(ssP, interface.name, includeSelf=false)
        .map(name => NameKindP(name))
    val (ssS2, descendantInterfacesTypesS) =
      compileKinds(ssP, ssS1, descendantInterfacesP)
    val descendantInterfacesS =
      descendantInterfacesTypesS.map({ case s @ InterfaceKindS(_, _) => s })

    val descendantStructsP =
      getDescendantStructs(ssP, interface.name)
        .map(name => NameKindP(name))
    val (ssS3, descendantStructsTypesS) =
      compileKinds(ssP, ssS2, descendantStructsP)
    val descendantStructsS =
      descendantStructsTypesS.map({ case s @ StructKindS(_, _) => s })

    val structsByInterfaceS =
      descendantInterfacesS
      .map(descendantInterfaceS => {
        val descendantStructsOfThisInterfaceS =
          ssS3.interfaces.find(_.name == descendantInterfaceS.name).get.descendantStructs
        (descendantInterfaceS -> descendantStructsOfThisInterfaceS)
      })
      .toMap

    val name =
      elementTypeS.name +
        (elementTypeS.ownership match { case OwnS => "" case StrongS => "Strong" case WeakS => "Weak" case ShareS => "" }) +
        (if (elementTypeS.nullable) "Maybe" else "") +
        (if (mutabilityS == MutableS) "Mut" else "Imm") +
        "Bunch"

    val (ssS6, descendantStructSetByStructS) =
      descendantStructsS.foldLeft((ssS3, Map[StructKindS, SetKindS]()))({
        case ((ssS4, previousStructSetsS), thisDescendantStructS) => {
          val (ssS5, thisDescendantStructSetS) =
            compileSet(ssP, ssS4, MutableS, TypeP(elementTypeS.nullable, uncompileOwnership(elementTypeS.ownership), NameKindP(thisDescendantStructS.name)))
          (ssS5, previousStructSetsS + (thisDescendantStructS -> thisDescendantStructSetS))
        }
      })

    val bunchStructS =
      StructS(
        name,
        false,
        mutabilityS,
        descendantStructsS.map(descendantStructS => {
          val descendantStructSetS = descendantStructSetByStructS(descendantStructS)
          StructMemberS(
            "members" + descendantStructSetS.name,
            FinalS,
            TypeS(false, OwnS, descendantStructSetS))
        }),
        List(),
        List(),
        List())
    val ssS7 = ssS6.addStruct(bunchStructS)
    val bunchStructTypeS = StructKindS(name, mutabilityS)

    val bunchS =
      BunchS(
        bunchStructTypeS,
        mutabilityS,
        interface,
        descendantStructsS,
        descendantStructSetByStructS,
        structsByInterfaceS)
    val ssS8 = ssS7.addBunch(bunchS)

    (ssS8, bunchStructTypeS)
  }

  def compileStruct(ssP: SuperstructureP, ssS0: SuperstructureS, structP: StructP): SuperstructureS = {
    val StructP(structName, isRoot, mutability, members, _, _) = structP

    val (ssS3, membersS) =
      members.foldLeft((ssS0, List[StructMemberS]()))({
        case ((ssS1, previousMembers), StructMemberP(memberName, variabilityP, typeP)) => {
          val (ssS2, typeS) = compileType(ssP, ssS1, typeP)
          val thisMember = StructMemberS(memberName, compileVariability(variabilityP), typeS)
          (ssS2, previousMembers :+ thisMember)
        }
      })

    val parentKindsP = getParentInterfaces(ssP, structName).map(NameKindP)
    val (ssS4, parentKindsS) = compileKinds(ssP, ssS3, parentKindsP)
    val parentInterfacesS = parentKindsS.map({ case i @ InterfaceKindS(_, _) => i })

    val ancestorKindsP = getAncestorInterfaces(ssP, structName, includeSelf=false).map(NameKindP)
    val (ssS5, ancestorKindsS) = compileKinds(ssP, ssS4, ancestorKindsP)
    val ancestorInterfacesS = ancestorKindsS.map({ case i @ InterfaceKindS(_, _) => i })

    val (ssS10, implsS) =
      ancestorInterfacesS
        .foldLeft((ssS5, List[ImplS]()))({
          case ((ssS6, previousImplsS), ancestorInterfaceS) => {
            val ancestorInterfaceAncestorsNames =
              ssP.getAncestorInterfaces(ancestorInterfaceS.name, true)
            val implSignaturesP =
              ssP
                .functions
                .filter(_.maybeOverriddenInterface.nonEmpty)
                .filter(signature => {
                  signature.maybeOverriddenInterface.get == ancestorInterfaceS.name ||
                  ancestorInterfaceAncestorsNames.contains(signature.maybeOverriddenInterface.get)
                })
                .filter(_.signature.parameters.nonEmpty)
                .filter(_.signature.parameters.head.tyype.kind == NameKindP(structName))
                .map(_.signature)
            println("impl sigs for " + structName + ":\n" + implSignaturesP.mkString("\n"))
            val (ssS9, implSignaturesS) =
              implSignaturesP.foldLeft((ssS6, List[SignatureS]()))({
                case ((ssS7, previousSignaturesS), thisSignatureP) => {
                  val (ssS8, thisSignatureS) = compileSignature(ssP, ssS7, thisSignatureP)
                  (ssS8, previousSignaturesS :+ thisSignatureS)
                }
              })
            (ssS9, previousImplsS :+ ImplS(ancestorInterfaceS, implSignaturesS))
          }
        })

    val structS =
      StructS(
        structName,
        isRoot,
        compileMutability(mutability),
        membersS,
        implsS,
        parentInterfacesS,
        ancestorInterfacesS)
    ssS10.addStruct(structS)
  }

  def compileInterface(ssP: SuperstructureP, ssS0: SuperstructureS, interfaceP: InterfaceP): SuperstructureS = {
    val InterfaceP(name, mutability, _, _) = interfaceP

    val parentKindsP = getParentInterfaces(ssP, name).map(NameKindP)
    val (ssS4, parentTypesS) = compileKinds(ssP, ssS0, parentKindsP)
    val parentInterfacesS = parentTypesS.map({ case i @ InterfaceKindS(_, _) => i })

    val ancestorKindsP = getAncestorInterfaces(ssP, name, includeSelf=false).map(NameKindP)
    val (ssS5, ancestorTypesS) = compileKinds(ssP, ssS4, ancestorKindsP)
    val ancestorInterfacesS = ancestorTypesS.map({ case i @ InterfaceKindS(_, _) => i })

    val childInterfacesP = getChildInterfaces(ssP, name).map(NameKindP)
    val (ssS6, childInterfaceTypesS) = compileKinds(ssP, ssS5, childInterfacesP)
    val childInterfacesS = childInterfaceTypesS.map({ case i @ InterfaceKindS(_, _) => i })

    val childStructsP = getChildStructs(ssP, name).map(NameKindP)
    val (ssS7, childStructsTypesS) = compileKinds(ssP, ssS6, childStructsP)
    val childStructsS = childStructsTypesS.map({ case i @ StructKindS(_, _) => i })

    val descendantInterfacesP = getDescendantInterfaces(ssP, name, includeSelf=false).map(NameKindP)
    val (ssS8, descendantInterfaceTypesP) = compileKinds(ssP, ssS7, descendantInterfacesP)
    val descendantInterfacesS = descendantInterfaceTypesP.map({ case i @ InterfaceKindS(_, _) => i })

    val descendantStructsP = getDescendantStructs(ssP, name).map(NameKindP)
    val (ssS9, descendantStructTypesS) = compileKinds(ssP, ssS8, descendantStructsP)
    val descendantStructS = descendantStructTypesS.map({ case i @ StructKindS(_, _) => i })

    val methodsP =
      getAncestorInterfaces(ssP, name, true)
        .flatMap(ancestorInterfaceName => {
          ssP.interfaces.find(_.name == ancestorInterfaceName).get.methods
        })
        .distinct
    val (ssS12, methodsWithDuplicatesS) =
      methodsP
      .foldLeft((ssS9, List[SignatureS]()))({
        case ((ssS10, previousSignatures), signatureP) => {
          val (ssS11, signatureS) = compileSignature(ssP, ssS10, signatureP)
          (ssS11, previousSignatures :+ signatureS)
        }
      })

    println("Interface " + name + " methods deduped: " + methodsWithDuplicatesS.distinct);

    val interfaceS =
      InterfaceS(
        name,
        compileMutability(mutability),
        methodsWithDuplicatesS.distinct,
        parentInterfacesS,
        ancestorInterfacesS,
        childInterfacesS,
        childStructsS,
        descendantInterfacesS,
        descendantStructS)
    ssS12.addInterface(interfaceS)
  }

  def compileFunction(ssP: SuperstructureP, ssS0: SuperstructureS, functionP: FunctionP):
  SuperstructureS = {
    val FunctionP(signatureP, externalFunctionName) = functionP
    val (ssS1, signatureS) = compileSignature(ssP, ssS0, signatureP)
    ssS1.addFunction(FunctionS(signatureS, externalFunctionName))
  }

  def compileSignature(ssP: SuperstructureP, ssS0: SuperstructureS, signatureP: SignatureP):
  (SuperstructureS, SignatureS) = {
    val SignatureP(funcName, returnTypeP, parametersP) = signatureP

    val (ssS3, parametersS) =
      parametersP.foldLeft((ssS0, List[ParameterS]()))({
        case ((ssS1, previousParameters), ParameterP(paramName, typeP, maybeOverride)) => {
          val (ssS2, typeS) = compileType(ssP, ssS1, typeP)
          val thisParameter = ParameterS(paramName, typeS, maybeOverride)
          (ssS2, previousParameters :+ thisParameter)
        }
      })

    val (ssS4, returnTypeS) = compileType(ssP, ssS3, returnTypeP)

    (ssS4, SignatureS(funcName, returnTypeS, parametersS))
  }

  def getParentInterfaces(ssP: SuperstructureP, sub: String): List[String] = {
    ssP.impls
      .filter(_.sub == sub)
      .map(_.interface)
      .distinct
  }

  def getAncestorInterfaces(
      ssP: SuperstructureP,
      sub: String,
      includeSelf: Boolean):
  List[String] = {
    val parents = getParentInterfaces(ssP, sub)
      ((if (includeSelf) List(sub) else List()) ++
      parents
        .flatMap(parent => getAncestorInterfaces(ssP, parent, includeSelf=true)))
      .distinct
  }

  def getChildInterfaces(ssP: SuperstructureP, parent: String): List[String] = {
    ssP.impls
      .filter(_.interface == parent)
      .map(_.sub)
      .filter(ssP.hasInterface)
      .distinct
  }

  def getChildStructs(ssP: SuperstructureP, parent: String): List[String] = {
    ssP.impls
      .filter(_.interface == parent)
      .map(_.sub)
      .filter(ssP.hasStruct)
      .distinct
  }

  def getDescendantInterfaces(
      ssP: SuperstructureP,
      parent: String,
      includeSelf: Boolean):
  List[String] = {
    val childInterfaces = getChildInterfaces(ssP, parent)
    ((if (includeSelf) List(parent) else List()) ++
    childInterfaces
        .flatMap(childInterface => getDescendantInterfaces(ssP, childInterface, includeSelf=true)))
      .distinct
  }

  def getDescendantStructs(ssP: SuperstructureP, parent: String): List[String] = {
    val selfAndDescendantInterfaces =
        getDescendantInterfaces(ssP, parent, includeSelf=true)
    selfAndDescendantInterfaces
        .flatMap(interface => getChildStructs(ssP, interface))
      .distinct
  }
}
