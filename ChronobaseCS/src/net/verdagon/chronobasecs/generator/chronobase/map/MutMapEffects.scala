package net.verdagon.chronobasecs.generator.chronobase.map

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutMapEffects {
  def generateEffects(opt: ChronobaseOptions, map: MapS): Map[String, String] = {
    val MapS(mapName, MutableS, keyType, elementType) = map

    val incarnationName = s"${mapName}Incarnation"
    val ieffectName = s"I${mapName}Effect"
    val observerName = s"I${mapName}EffectObserver"
    val visitorName = s"I${mapName}EffectVisitor"
    val createEffectName = s"${mapName}CreateEffect"
    val deleteEffectName = s"${mapName}DeleteEffect"
    val addEffectName = s"${mapName}AddEffect"
    val removeEffectName = s"${mapName}RemoveEffect"

    val keyCSType = toCS(keyType)
    val elementCSType = toCS(elementType)
    val flattenedKeyCSType = toCS(keyType.flatten)
    val flattenedElementCSType = toCS(elementType.flatten)

    val observerDefinition =
        s"""
           |public interface ${observerName} {
           |  void On${mapName}Effect(I${mapName}Effect effect);
           |}
           |""".stripMargin

    val ieffectDefinition =
        s"""
           |public interface ${ieffectName} {
           |  int id { get; }
           |  void visit(${visitorName} visitor);
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
           |  public readonly ${flattenedKeyCSType} key;
           |  public readonly ${flattenedElementCSType} value;
           |  public ${addEffectName}(int id, ${flattenedKeyCSType} key, ${flattenedElementCSType} value) {
           |    this.id = id;
           |    this.key = key;
           |    this.value = value;
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
           |  public readonly ${flattenedKeyCSType} key;
           |  public ${removeEffectName}(int id, ${flattenedKeyCSType} key) {
           |    this.id = id;
           |    this.key = key;
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

  def generateRootMembers(opt: ChronobaseOptions, map: MapS): String = {
    val MapS(mapName, MutableS, keyType, elementType) = map

    val createEffectName = s"${mapName}CreateEffect"
    val deleteEffectName = s"${mapName}DeleteEffect"
    val addEffectName = s"${mapName}AddEffect"
    val removeEffectName = s"${mapName}RemoveEffect"

    List(createEffectName, deleteEffectName, addEffectName, removeEffectName)
      .map(effectCSType => {
        s"""  readonly List<${effectCSType}> effects${effectCSType} =
           |      new List<${effectCSType}>();
           |""".stripMargin
      })
      .mkString("")
  }
}
