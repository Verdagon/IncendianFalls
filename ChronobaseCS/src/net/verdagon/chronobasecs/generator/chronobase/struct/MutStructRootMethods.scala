package net.verdagon.chronobasecs.generator.chronobase.struct

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.MutStructEffects.getEffectsCSTypes
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutStructRootMethods {

  def generateRootStructInstanceCreateMethod(
                                              opt: ChronobaseOptions,
                                              struct: StructS
  ): String = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct
    s"  public ${structName} Effect${structName}Create(\n" +
      members.map({ case StructMemberS(memberName, variability, memberType) =>
        "      " + toCS(memberType) + " " + memberName
      }).mkString(",\n") +
      ") {\n" +
      "    CheckUnlocked();\n" +
      members
        .flatMap({ case StructMemberS(memberName, variability, memberType) =>
          if (memberType.kind.mutability == MutableS && !memberType.nullable) {
            val memberCSType = toCS(memberType)
            List(s"    CheckHas${memberCSType}(${memberName});\n")
          } else {
            List()
          }
        })
        .mkString("") +
    s"""
       |    var id = NewId();
       |    var incarnation =
       |        new ${structName}Incarnation(
       |""".stripMargin +
    members.map({ case StructMemberS(memberName, variability, memberType) =>
      if (memberType.kind.mutability == MutableS) {
        s"            ${memberName}.id"
      } else {
        s"            ${memberName}"
      }
    }).mkString(",\n") +
    s"""
       |            );
       |    EffectInternalCreate${structName}(id, rootIncarnation.version, incarnation);
       |    return new ${structName}(this, id);
       |  }
       |  public void EffectInternalCreate${structName}(
       |      int id,
       |      int incarnationVersion,
       |      ${structName}Incarnation incarnation) {
       |    CheckUnlocked();
       |    var effect = new ${structName}CreateEffect(id);
       |    rootIncarnation.incarnations${structName}.Add(
       |        id,
       |        new VersionAndIncarnation<${structName}Incarnation>(
       |            incarnationVersion,
       |            incarnation));
       |""".stripMargin +
      (if (opt.hash) {
        s"""    this.rootIncarnation.hash += Get${structName}Hash(id, incarnationVersion, incarnation);
       |
           |""".stripMargin
      } else "") +
      s"""    effects${structName}CreateEffect.Add(effect);
       |  }
       |""".stripMargin
  }
  def generateRootStructInstanceHashMethod(
                                            opt: ChronobaseOptions,
                                            struct: StructS
  ): String = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct
    s"""
       |  public int Get${structName}Hash(int id, int version, ${structName}Incarnation incarnation) {
       |    int result = id * version;
       |""".stripMargin +
    members.zipWithIndex.map({ case (StructMemberS(memberName, variability, memberType), index) =>
      if (memberType.nullable) {
        s"""    if (!object.ReferenceEquals(incarnation.${memberName}, null)) {
           |      result += id * version * ${index + 1} * incarnation.${memberName}.GetDeterministicHashCode();
           |    }
           |""".stripMargin
      } else {
        s"""    result += id * version * ${index + 1} * incarnation.${memberName}.GetDeterministicHashCode();
           |""".stripMargin
      }
    }).mkString("") +
    s"""    return result;
       |  }
     """.stripMargin
  }

  def generateRootStructInstanceDeleteMethod(
                                              opt: ChronobaseOptions,
                                              struct: StructS
  ): String = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct
    s"""
       |  public void Effect${structName}Delete(int id) {
       |    CheckUnlocked();
       |    var effect = new ${structName}DeleteEffect(id);
       |
       |    var oldIncarnationAndVersion =
       |        rootIncarnation.incarnations${structName}[id];
       |""".stripMargin +
      (if (opt.hash) {
        s"""    this.rootIncarnation.hash -=
       |        Get${structName}Hash(
       |            id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
       |
           |""".stripMargin
      } else "") +
      s"""
       |    rootIncarnation.incarnations${structName}.Remove(id);
       |    effects${structName}DeleteEffect.Add(effect);
       |  }
       |
     """.stripMargin
  }

  def generateRootStructInstanceSetterMethod(
                                              opt: ChronobaseOptions,
                                              struct: StructS,
                                              memberIndex: Int,
                                              memberName: String,
                                              memberType: TypeS[IKindS]) = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct
    s"""
       |  public void Effect${struct.name}Set${memberName.capitalize}(int id, ${toCS(memberType)} newValue) {
       |    CheckUnlocked();
       |    CheckHas${structName}(id);
       |    var effect = new ${structName}Set${memberName.capitalize}Effect(id, newValue);
       |    var oldIncarnationAndVersion = rootIncarnation.incarnations${structName}[id];
       |    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
       |""".stripMargin +
      (memberType.kind.mutability match {
        case MutableS => {
          s"""      var oldId = oldIncarnationAndVersion.incarnation.${memberName};
             |      oldIncarnationAndVersion.incarnation.${memberName} = newValue.id;
             |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash -= id * rootIncarnation.version * ${memberIndex + 1} * oldId.GetDeterministicHashCode();
             |      this.rootIncarnation.hash += id * rootIncarnation.version * ${memberIndex + 1} * newValue.id.GetDeterministicHashCode();
             |
           |""".stripMargin
      } else "") +
      s"""""".stripMargin
        }
        case ImmutableS => {
          s"""      var oldValue = oldIncarnationAndVersion.incarnation.${memberName};
             |      oldIncarnationAndVersion.incarnation.${memberName} = newValue;
             |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash -= id * rootIncarnation.version * ${memberIndex + 1} * oldValue.GetDeterministicHashCode();
             |      this.rootIncarnation.hash += id * rootIncarnation.version * ${memberIndex + 1} * newValue.GetDeterministicHashCode();
             |
           |""".stripMargin
      } else "") +
      s"""""".stripMargin
        }
      }) +
      s"""
         |    } else {
         |      var newIncarnation =
         |          new ${structName}Incarnation(
         |""".stripMargin +
      members
        .map(_.name)
        .map(constructorArgMemberName => {
          if (memberName == constructorArgMemberName) {
            memberType.kind.mutability match {
              case MutableS => "              newValue.id"
              case ImmutableS => "              newValue"
            }
          } else {
            "              oldIncarnationAndVersion.incarnation." + constructorArgMemberName
          }
        })
        .mkString(",\n") +
      s""");
         |      rootIncarnation.incarnations${structName}[id] =
         |          new VersionAndIncarnation<${structName}Incarnation>(
         |              rootIncarnation.version,
         |              newIncarnation);
         |""".stripMargin +
      (if (opt.hash) {
        s"""      this.rootIncarnation.hash -= Get${structName}Hash(id, oldIncarnationAndVersion.version, oldIncarnationAndVersion.incarnation);
         |      this.rootIncarnation.hash += Get${structName}Hash(id, rootIncarnation.version, newIncarnation);
         |
           |""".stripMargin
      } else "") +
      s"""    }
         |
         |    effects${structName}Set${memberName.capitalize}Effect.Add(effect);
         |  }
         |""".stripMargin
  }

  def generateRootStructInstanceMethods(
                                         opt: ChronobaseOptions,
                                         struct: StructS
  ): String = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct


    s"""  public ${structName}Incarnation Get${structName}Incarnation(int id) {
       |    if (id == 0) {
       |      throw new Exception("Tried dereferencing null!");
       |    }
       |    return rootIncarnation.incarnations${structName}[id].incarnation;
       |  }
       |  public bool ${structName}Exists(int id) {
       |    return rootIncarnation.incarnations${structName}.ContainsKey(id);
       |  }
       |  public ${structName} Get${structName}(int id) {
       |    return new ${structName}(this, id);
       |  }
       |  public List<${structName}> All${structName}() {
       |    List<${structName}> result = new List<${structName}>(rootIncarnation.incarnations${structName}.Count);
       |    foreach (var id in rootIncarnation.incarnations${structName}.Keys) {
       |      result.Add(new ${structName}(this, id));
       |    }
       |    return result;
       |  }
       |  public IEnumerator<${structName}> EnumAll${structName}() {
       |    foreach (var id in rootIncarnation.incarnations${structName}.Keys) {
       |      yield return Get${structName}(id);
       |    }
       |  }
       |  public void CheckHas${structName}(${structName} thing) {
       |    CheckRootsEqual(this, thing.root);
       |    CheckHas${structName}(thing.id);
       |  }
       |  public void CheckHas${structName}(int id) {
       |    if (!rootIncarnation.incarnations${structName}.ContainsKey(id)) {
       |      throw new System.Exception("Invalid ${structName}: " + id);
       |    }
       |  }
       |  public void Add${structName}Observer(int id, I${structName}EffectObserver observer) {
       |    List<I${structName}EffectObserver> obsies;
       |    if (!observersFor${structName}.TryGetValue(id, out obsies)) {
       |      obsies = new List<I${structName}EffectObserver>();
       |    }
       |    obsies.Add(observer);
       |    observersFor${structName}[id] = obsies;
       |  }
       |
       |  public void Remove${structName}Observer(int id, I${structName}EffectObserver observer) {
       |    if (observersFor${structName}.ContainsKey(id)) {
       |      var list = observersFor${structName}[id];
       |      list.Remove(observer);
       |      if (list.Count == 0) {
       |        observersFor${structName}.Remove(id);
       |      }
       |    } else {
       |      throw new Exception("Couldnt find!");
       |    }
       |  }
       |""".stripMargin +
      generateRootStructInstanceCreateMethod(opt, struct) +
      generateRootStructInstanceDeleteMethod(opt, struct) +
      generateRootStructInstanceHashMethod(opt, struct) +
      generateBroadcaster(opt, struct) +
      members.zipWithIndex.map({
        case (StructMemberS(memberName, FinalS, memberType), memberIndex) => ""
        case (StructMemberS(memberName, VaryingS, memberType), memberIndex) =>
          generateRootStructInstanceSetterMethod(opt, struct, memberIndex, memberName, memberType)
      }).mkString("")
  }

  def generateBroadcaster(opt: ChronobaseOptions, struct: StructS): String = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct

    val observerName = s"I${structName}EffectObserver"
    val structCSType = toCS(struct.tyype)

    val deleteEffectName = s"${structCSType}DeleteEffect"
    val createEffectName = s"${structCSType}CreateEffect"

    // Delete has to be first. This is so it can clear away all those
    // observers observing this object, so they don't have to remove
    // themselves, and if something is ressurrected via revert, the
    // observers for the old existence won't be notified.
    s"""
       |  public void Broadcast${structName}Effects(
       |      SortedDictionary<int, List<I${structCSType}EffectObserver>> observers) {
       |    foreach (var effect in effects${deleteEffectName}) {
       |      if (observers.TryGetValue(0, out List<${observerName}> globalObservers)) {
       |        foreach (var observer in globalObservers) {
       |          observer.On${structCSType}Effect(effect);
       |        }
       |      }
       |      if (observers.TryGetValue(effect.id, out List<${observerName}> objObservers)) {
       |        foreach (var observer in objObservers) {
       |          observer.On${structCSType}Effect(effect);
       |        }
       |        observersFor${structCSType}.Remove(effect.id);
       |      }
       |    }
       |    effects${deleteEffectName}.Clear();
       |
       |""".stripMargin +
      struct.members.filter(_.variability == VaryingS).map(_.name)
      .map(memberName => {
        val effectCSType = s"${structCSType}Set${memberName.capitalize}Effect";
        s"""
           |    foreach (var effect in effects${effectCSType}) {
           |      if (observers.TryGetValue(0, out List<${observerName}> globalObservers)) {
           |        foreach (var observer in globalObservers) {
           |          observer.On${structCSType}Effect(effect);
           |        }
           |      }
           |      if (observers.TryGetValue(effect.id, out List<${observerName}> objObservers)) {
           |        foreach (var observer in objObservers) {
           |          observer.On${structCSType}Effect(effect);
           |        }
           |      }
           |    }
           |    effects${effectCSType}.Clear();
           |""".stripMargin
      })
      .mkString("") +
    s"""
       |    foreach (var effect in effects${createEffectName}) {
       |      if (observers.TryGetValue(0, out List<${observerName}> globalObservers)) {
       |        foreach (var observer in globalObservers) {
       |          observer.On${structCSType}Effect(effect);
       |        }
       |      }
       |      if (observers.TryGetValue(effect.id, out List<${observerName}> objObservers)) {
       |        foreach (var observer in objObservers) {
       |          observer.On${structCSType}Effect(effect);
       |        }
       |      }
       |    }
       |    effects${createEffectName}.Clear();
       |  }
       |""".stripMargin
  }

}
