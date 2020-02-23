package net.verdagon.chronobasecs.generator.chronobase.set

import net.verdagon.chronobasecs.compiled.{ImmutableS, MutableS, OwnS, SetS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutSetEffects {

  def generateEffects(opt: ChronobaseOptions, set: SetS): Map[String, String] = {
    val SetS(setName, MutableS, elementType) = set

    val incarnationName = s"${setName}Incarnation"
    val ieffectName = s"I${setName}Effect"
    val observerName = s"I${setName}EffectObserver"
    val visitorName = s"I${setName}EffectVisitor"
    val createEffectName = s"${setName}CreateEffect"
    val deleteEffectName = s"${setName}DeleteEffect"
    val addEffectName = s"${setName}AddEffect"
    val removeEffectName = s"${setName}RemoveEffect"

    val elementCSType = toCS(elementType)

    val observerDefinition =
      s"""public interface ${observerName} {
         |  void On${setName}Effect(I${setName}Effect effect);
         |}
         |""".stripMargin

    val ieffectDefinition =
      s"""public interface ${ieffectName} {
         |  int id { get; }
         |  void visit(${visitorName} visitor);
         |}
         |""".stripMargin

    val visitorDefinition =
      s"""public interface ${visitorName} {
         |  void visit${createEffectName}(${createEffectName} effect);
         |  void visit${deleteEffectName}(${deleteEffectName} effect);
         |  void visit${addEffectName}(${addEffectName} effect);
         |  void visit${removeEffectName}(${removeEffectName} effect);
         |}
         """.stripMargin

    val createEffectDefinition =
      s"""public struct ${createEffectName} : ${ieffectName} {
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
      s"""public struct ${deleteEffectName} : ${ieffectName} {
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

    val addEffectDefinition =
      s"""public struct ${addEffectName} : ${ieffectName} {
         |  public readonly int id;
         |  public readonly int elementId;
         |  public ${addEffectName}(int id, int elementId) {
         |    this.id = id;
         |    this.elementId = elementId;
         |  }
         |  int ${ieffectName}.id => id;
         |  public void visit(${visitorName} visitor) {
         |    visitor.visit${addEffectName}(this);
         |  }
         |}
         |""".stripMargin

    val removeEffectDefinition =
      s"""public struct ${removeEffectName} : ${ieffectName} {
         |  public readonly int id;
         |  public readonly int elementId;
         |  public ${removeEffectName}(int id, int elementId) {
         |    this.id = id;
         |    this.elementId = elementId;
         |  }
         |  int ${ieffectName}.id => id;
         |  public void visit(${visitorName} visitor) {
         |    visitor.visit${removeEffectName}(this);
         |  }
         |}
         |""".stripMargin

    Map(
      ieffectName -> ieffectDefinition,
      observerName -> observerDefinition,
      visitorName -> visitorDefinition,
      createEffectName -> createEffectDefinition,
      deleteEffectName -> deleteEffectDefinition,
      addEffectName -> addEffectDefinition,
      removeEffectName -> removeEffectDefinition)
  }

  def generateRootMembers(opt: ChronobaseOptions, set: SetS): String = {
    val SetS(setName, MutableS, elementType) = set

    val createEffectName = s"${setName}CreateEffect"
    val deleteEffectName = s"${setName}DeleteEffect"
    val addEffectName = s"${setName}AddEffect"
    val removeEffectName = s"${setName}RemoveEffect"

    List(createEffectName, deleteEffectName, addEffectName, removeEffectName)
      .map(effectCSType => {
        s"""  readonly List<${effectCSType}> effects${effectCSType} =
           |      new List<${effectCSType}>();
           |""".stripMargin
      })
      .mkString("")
  }
}
