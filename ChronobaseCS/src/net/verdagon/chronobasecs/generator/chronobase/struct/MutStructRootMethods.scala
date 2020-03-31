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
    s"    return TrustedEffect${structName}CreateWithId(NewId()\n" +
    members.map({ case StructMemberS(memberName, variability, memberType) =>
      if (memberType.kind.mutability == MutableS) {
        s"            ,${memberName}.id"
      } else {
        s"            ,${memberName}"
      }
    }).mkString("\n") +
      "    );\n" +
      "  }\n" +
    s"  public ${structName} TrustedEffect${structName}CreateWithId(int id\n" +
      members.map({ case StructMemberS(memberName, variability, memberType) =>
        ",      " + toCS(memberType.flatten) + " " + memberName
      }).mkString("\n") +
      ") {\n" +
      "    CheckUnlocked();\n" +
    s"""
       |    var incarnation =
       |        new ${structName}Incarnation(
       |""".stripMargin +
    members.map({ case StructMemberS(memberName, variability, memberType) =>
        s"            ${memberName}"
    }).mkString(",\n") +
    s"""
       |            );
       |    return EffectInternalCreate${structName}(id, rootIncarnation.version, incarnation);
       |  }
       |  public ${structName} EffectInternalCreate${structName}(
       |      int id,
       |      int incarnationVersion,
       |      ${structName}Incarnation incarnation) {
       |    CheckUnlocked();
       |    var effect = new ${structName}CreateEffect(id, incarnation.Copy());
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
      s"""    NotifyEffect(effect);
         |    return new ${structName}(this, id);
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
       |    NotifyEffect(effect);
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
       |""".stripMargin +
      (memberType.kind.mutability match {
        case MutableS => s"var effect = new ${structName}Set${memberName.capitalize}Effect(id, newValue.id);"
        case ImmutableS => s"var effect = new ${structName}Set${memberName.capitalize}Effect(id, newValue);"
      }) +
    s"""
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
         |    NotifyEffect(effect);
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
       |    CheckHas${structName}(id);
       |    return new ${structName}(this, id);
       |  }
       |  public ${structName} Get${structName}OrNull(int id) {
       |    if (${structName}Exists(id)) {
       |      return new ${structName}(this, id);
       |    } else {
       |      return new ${structName}(this, 0);
       |    }
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
       |""".stripMargin +
      generateRootStructInstanceCreateMethod(opt, struct) +
      generateRootStructInstanceDeleteMethod(opt, struct) +
      generateRootStructInstanceHashMethod(opt, struct) +
      members.zipWithIndex.map({
        case (StructMemberS(memberName, FinalS, memberType), memberIndex) => ""
        case (StructMemberS(memberName, VaryingS, memberType), memberIndex) =>
          generateRootStructInstanceSetterMethod(opt, struct, memberIndex, memberName, memberType)
      }).mkString("")
  }
}
