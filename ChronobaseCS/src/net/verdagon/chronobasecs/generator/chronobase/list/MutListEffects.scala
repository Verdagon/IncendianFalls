package net.verdagon.chronobasecs.generator.chronobase.list

import net.verdagon.chronobasecs.compiled.{ImmutableS, ListS, MutableS, OwnS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutListEffects {

  def generateEffects(opt: ChronobaseOptions, list: ListS): Map[String, String] = {
    val ListS(listName, MutableS, elementType) = list

    val incarnationName = s"${listName}Incarnation"
    val ieffectName = s"I${listName}Effect"
    val observerName = s"I${listName}EffectObserver"
    val visitorName = s"I${listName}EffectVisitor"
    val createEffectName = s"${listName}CreateEffect"
    val deleteEffectName = s"${listName}DeleteEffect"
    val addEffectName = s"${listName}AddEffect"
    val removeEffectName = s"${listName}RemoveEffect"

    val flattenedElementCSType = toCS(elementType.flatten)
    val elementCSType = toCS(elementType)

    val observerDefinition =
      s"""
         |public interface ${observerName} {
         |  void On${listName}Effect(I${listName}Effect effect);
         |}
         |""".stripMargin

    val visitorDefinition =
      s"""
         |public interface ${visitorName} {
         |  void visit${createEffectName}(${createEffectName} effect);
         |  void visit${deleteEffectName}(${deleteEffectName} effect);
         |  void visit${addEffectName}(${addEffectName} effect);
         |  void visit${removeEffectName}(${removeEffectName} effect);
         |}
         """.stripMargin

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

    val addEffectDefinition =
        s"""
           |public struct ${addEffectName} : ${ieffectName} {
           |  public readonly int id;
           |  public readonly ${flattenedElementCSType} element;
           |  public ${addEffectName}(int id, ${flattenedElementCSType} element) {
           |    this.id = id;
           |    this.element = element;
           |  }
           |  int ${ieffectName}.id => id;
           |  public void visit(${visitorName} visitor) {
           |    visitor.visit${addEffectName}(this);
           |  }
           |}
           |""".stripMargin

    val removeEffectDefinition =
        s"""
           |public struct ${removeEffectName} : ${ieffectName} {
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

    val ieffectDefinition =
      s"""
         |public interface ${ieffectName} {
         |  int id { get; }
         |  void visit(${visitorName} visitor);
         |}
         |""".stripMargin

    Map(
      observerName -> observerDefinition,
      visitorName -> visitorDefinition,
      ieffectName -> ieffectDefinition,
      createEffectName -> createEffectDefinition,
      deleteEffectName -> deleteEffectDefinition,
      addEffectName -> addEffectDefinition,
      removeEffectName -> removeEffectDefinition)
  }

  def generateRootMembers(opt: ChronobaseOptions, list: ListS): String = {
    val ListS(listName, MutableS, elementType) = list

    val createEffectName = s"${listName}CreateEffect"
    val deleteEffectName = s"${listName}DeleteEffect"
    val addEffectName = s"${listName}AddEffect"
    val removeEffectName = s"${listName}RemoveEffect"

    List(createEffectName, deleteEffectName, addEffectName, removeEffectName)
      .map(effectCSType => {
        s"""  readonly List<${effectCSType}> effects${effectCSType} =
           |      new List<${effectCSType}>();
           |""".stripMargin
      })
      .mkString("")
  }
}
