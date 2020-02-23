package net.verdagon.chronobasecs.generator

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutStructEffects {

  def generateEffects(opt: ChronobaseOptions, struct: StructS): Map[String, String] = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct

    val ieffectName = s"I${structName}Effect";
    val observerName = s"I${structName}EffectObserver"
    val visitorName = s"I${structName}EffectVisitor";
    val createEffectName = s"${structName}CreateEffect";
    val deleteEffectName = s"${structName}DeleteEffect";

    val ieffectDefinition =
      s"""
         |public interface ${ieffectName} {
         |  int id { get; }
         |  void visit(${visitorName} visitor);
         |}
       """.stripMargin

    val observerDefinition =
      s"""
         |public interface ${observerName} {
         |  void On${structName}Effect(I${structName}Effect effect);
         |}
         |""".stripMargin

    val visitorDefinition =
      s"public interface ${visitorName} {\n" +
      s"  void visit${createEffectName}(${createEffectName} effect);\n" +
      s"  void visit${deleteEffectName}(${deleteEffectName} effect);\n" +
        members.map({
          case StructMemberS(_, FinalS, _) => ""
          case StructMemberS(memberName, VaryingS, _) => {
            val effectName = s"${structName}Set${memberName.capitalize}Effect"
            s"  void visit${effectName}(${effectName} effect);\n"
          }
        }).mkString("") +
        s"}\n"

    val createEffectDefinition =
      s"""
         |public struct ${createEffectName} : ${ieffectName} {
         |  public readonly int id;
         |  public ${createEffectName}(int id) {
         |    this.id = id;
         |  }
         |  int ${ieffectName}.id => id;
         |  public void visit(${visitorName} visitor) {
         |    visitor.visit${createEffectName}(this);
         |  }
         |}
         |""".stripMargin

    val deleteEffectDefinition =
      s"""
         |public struct ${deleteEffectName} : ${ieffectName} {
         |  public readonly int id;
         |  public ${deleteEffectName}(int id) {
         |    this.id = id;
         |  }
         |  int ${ieffectName}.id => id;
         |  public void visit(${visitorName} visitor) {
         |    visitor.visit${deleteEffectName}(this);
         |  }
         |}
         |""".stripMargin

    Map(
      ieffectName -> ieffectDefinition,
      observerName -> observerDefinition,
      visitorName -> visitorDefinition,
      createEffectName -> createEffectDefinition,
      deleteEffectName -> deleteEffectDefinition) ++
      members.flatMap({
        case StructMemberS(memberName, FinalS, memberType) => Map[String, String]()
        case StructMemberS(memberName, VaryingS, memberType) => {
          val effectName = s"${structName}Set${memberName.capitalize}Effect"
          val effectDefinition =
            s"""
               |public struct ${effectName} : ${ieffectName} {
               |  public readonly int id;
               |  public readonly ${toCS(memberType)} newValue;
               |  public ${effectName}(
               |      int id,
               |      ${toCS(memberType)} newValue) {
               |    this.id = id;
               |    this.newValue = newValue;
               |  }
               |  int ${ieffectName}.id => id;
               |
               |  public void visit(${visitorName} visitor) {
               |    visitor.visit${effectName}(this);
               |  }
               |}
               |""".stripMargin
          Map(effectName -> effectDefinition)
        }
      }).toMap
  }

  def getEffectsCSTypes(struct: StructS): List[String] = {
    val structCSType = toCS(struct.tyype)
    List(
      s"${structCSType}CreateEffect",
      s"${structCSType}DeleteEffect") ++
      struct.members.flatMap({
        case StructMemberS(memberName, FinalS, memberType) => List()
        case StructMemberS(memberName, VaryingS, memberType) => {
          List(s"${structCSType}Set${memberName.capitalize}Effect")
        }
      })
  }

  def generateRootMembers(opt: ChronobaseOptions, struct: StructS): String = {
    getEffectsCSTypes(struct)
      .map(effectCSType => {
        s"""  readonly List<${effectCSType}> effects${effectCSType} =
           |      new List<${effectCSType}>();
           |""".stripMargin
      })
      .mkString("")
  }
}
