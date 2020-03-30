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
           |public interface ${ieffectName} : IEffect {
           |  int id { get; }
           |  void visit${ieffectName}(${visitorName} visitor);
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
           |  public void visit${ieffectName}(${visitorName} visitor) {
           |    visitor.visit${createEffectName}(this);
           |  }
           |  public void visitIEffect(IEffectVisitor visitor) {
           |    visitor.visit${mapName}Effect(this);
           |  }
           |  public bool isSubtractive() { return false; }
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
           |  public void visit${ieffectName}(${visitorName} visitor) {
           |    visitor.visit${deleteEffectName}(this);
           |  }
           |  public void visitIEffect(IEffectVisitor visitor) {
           |    visitor.visit${mapName}Effect(this);
           |  }
           |  public bool isSubtractive() { return true; }
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
           |  public void visit${ieffectName}(${visitorName} visitor) {
           |    visitor.visit${addEffectName}(this);
           |  }
           |  public void visitIEffect(IEffectVisitor visitor) {
           |    visitor.visit${mapName}Effect(this);
           |  }
           |  public bool isSubtractive() { return false; }
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
           |  public void visit${ieffectName}(${visitorName} visitor) {
           |    visitor.visit${removeEffectName}(this);
           |  }
           |  public void visitIEffect(IEffectVisitor visitor) {
           |    visitor.visit${mapName}Effect(this);
           |  }
           |  public bool isSubtractive() { return true; }
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

  def generateGlobalVisitorInterfaceMethods(map: MapS) = {
    val MapS(mapName, MutableS, keyType, elementType) = map

    val ieffectName = s"I${mapName}Effect"

    s"void visit${mapName}Effect(${ieffectName} effect);\n"
  }

  def generateEffectBroadcasterMethods(map: MapS) = {
    val MapS(mapName, MutableS, keyType, elementType) = map

    val mapCSType = toCS(map.tyype)
    val ieffectName = s"I${mapName}Effect"

    s"""
       |  public void visit${mapName}Effect(${ieffectName} effect) {
       |    if (observersFor${mapCSType}.TryGetValue(effect.id, out var observers)) {
       |      foreach (var observer in new List<I${mapCSType}EffectObserver>(observers)) {
       |        observer.On${mapCSType}Effect(effect);
       |      }
       |    }
       |  }
       |    public void Add${mapName}Observer(int id, I${mapName}EffectObserver observer) {
       |      List<I${mapName}EffectObserver> obsies;
       |      if (!observersFor${mapName}.TryGetValue(id, out obsies)) {
       |        obsies = new List<I${mapName}EffectObserver>();
       |      }
       |      obsies.Add(observer);
       |      observersFor${mapName}[id] = obsies;
       |    }
       |    public void Remove${mapName}Observer(int id, I${mapName}EffectObserver observer) {
       |      if (observersFor${mapName}.ContainsKey(id)) {
       |        var map = observersFor${mapName}[id];
       |        map.Remove(observer);
       |        if (map.Count == 0) {
       |          observersFor${mapName}.Remove(id);
       |        }
       |      } else {
       |        throw new Exception("Couldnt find!");
       |      }
       |    }
       |""".stripMargin
  }

  def generateEffectApplierMethods(map: MapS): String = {
    val MapS(mapName, MutableS, keyType, elementType) = map

    val createEffectName = s"${mapName}CreateEffect"
    val deleteEffectName = s"${mapName}DeleteEffect"
    val addEffectName = s"${mapName}AddEffect"
    val removeEffectName = s"${mapName}RemoveEffect"
    val elementCSType = toCS(elementType)
    val ieffectName = s"I${mapName}Effect"

    s"""
       |    public void visit${mapName}Effect(I${mapName}Effect effect) { effect.visit${ieffectName}(this); }
       |    public void visit${createEffectName}(${createEffectName} effect) {
       |      // For now we're just feeding the remote ID in. Someday we might want to have a map
       |      // in the applier instead.
       |      root.Effect${mapName}CreateWithId(effect.id);
       |    }
       |    public void visit${deleteEffectName}(${deleteEffectName} effect) {
       |      root.Effect${mapName}Delete(effect.id);
       |    }
       |    public void visit${addEffectName}(${addEffectName} effect) {
       |      root.Effect${mapName}Add(effect.id, effect.key, effect.value);
       |    }
       |    public void visit${removeEffectName}(${removeEffectName} effect) {
       |      root.CheckUnlocked();
       |      root.Effect${mapName}Remove(effect.id, effect.key);
       |    }
     """.stripMargin
  }
}
