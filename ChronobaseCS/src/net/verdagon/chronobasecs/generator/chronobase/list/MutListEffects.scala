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
           |  public void visit${ieffectName}(${visitorName} visitor) {
           |    visitor.visit${createEffectName}(this);
           |  }
           |  public void visitIEffect(IEffectVisitor visitor) {
           |    visitor.visit${listName}Effect(this);
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
           |    visitor.visit${listName}Effect(this);
           |  }
           |  public bool isSubtractive() { return true; }
           |}
           |""".stripMargin

    val addEffectDefinition =
        s"""
           |public struct ${addEffectName} : ${ieffectName} {
           |  public readonly int id;
           |  public readonly int index;
           |  public readonly ${flattenedElementCSType} element;
           |  public ${addEffectName}(int id, int index, ${flattenedElementCSType} element) {
           |    this.id = id;
           |    this.index = index;
           |    this.element = element;
           |  }
           |  int ${ieffectName}.id => id;
           |  public void visit${ieffectName}(${visitorName} visitor) {
           |    visitor.visit${addEffectName}(this);
           |  }
           |  public void visitIEffect(IEffectVisitor visitor) {
           |    visitor.visit${listName}Effect(this);
           |  }
           |  public bool isSubtractive() { return false; }
           |}
           |""".stripMargin

    val removeEffectDefinition =
        s"""
           |public struct ${removeEffectName} : ${ieffectName} {
           |  public readonly int id;
           |  public readonly int index;
           |  public ${removeEffectName}(int id, int index) {
           |    this.id = id;
           |    this.index = index;
           |  }
           |  int ${ieffectName}.id => id;
           |  public void visit${ieffectName}(${visitorName} visitor) {
           |    visitor.visit${removeEffectName}(this);
           |  }
           |  public void visitIEffect(IEffectVisitor visitor) {
           |    visitor.visit${listName}Effect(this);
           |  }
           |  public bool isSubtractive() { return true; }
           |}
           |""".stripMargin

    val ieffectDefinition =
      s"""
         |public interface ${ieffectName} : IEffect {
         |  int id { get; }
         |  void visit${ieffectName}(${visitorName} visitor);
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


  def generateGlobalVisitorInterfaceMethods(list: ListS) = {
    val ListS(setName, MutableS, elementType) = list

    val ieffectName = s"I${setName}Effect"

    s"void visit${setName}Effect(${ieffectName} effect);\n"
  }

  def generateEffectBroadcasterMethods(list: ListS) = {
    val ListS(listName, MutableS, elementType) = list

    val listCSType = toCS(list.tyype)
    val ieffectName = s"I${listName}Effect"

    s"""
       |    public void visit${listName}Effect(${ieffectName} effect) {
       |      if (observersFor${listCSType}.TryGetValue(effect.id, out var observers)) {
       |        foreach (var observer in new List<I${listCSType}EffectObserver>(observers)) {
       |          observer.On${listCSType}Effect(effect);
       |        }
       |      }
       |    }
       |    public void Add${listName}Observer(int id, I${listName}EffectObserver observer) {
       |      List<I${listName}EffectObserver> obsies;
       |      if (!observersFor${listName}.TryGetValue(id, out obsies)) {
       |        obsies = new List<I${listName}EffectObserver>();
       |      }
       |      obsies.Add(observer);
       |      observersFor${listName}[id] = obsies;
       |    }
       |    public void Remove${listName}Observer(int id, I${listName}EffectObserver observer) {
       |      if (observersFor${listName}.ContainsKey(id)) {
       |        var map = observersFor${listName}[id];
       |        map.Remove(observer);
       |        if (map.Count == 0) {
       |          observersFor${listName}.Remove(id);
       |        }
       |      } else {
       |        throw new Exception("Couldnt find!");
       |      }
       |    }
       |""".stripMargin
  }

  def generateEffectApplierMethods(list: ListS) = {
    val ListS(listName, MutableS, elementType) = list

    val createEffectName = s"${listName}CreateEffect"
    val deleteEffectName = s"${listName}DeleteEffect"
    val addEffectName = s"${listName}AddEffect"
    val removeEffectName = s"${listName}RemoveEffect"
    val elementCSType = toCS(elementType)
    val ieffectName = s"I${listName}Effect"

    s"""
       |    public void visit${listName}Effect(I${listName}Effect effect) { effect.visit${ieffectName}(this); }
       |    public void visit${createEffectName}(${createEffectName} effect) {
       |      // For now we're just feeding the remote ID in. Someday we might want to have a map
       |      // in the applier instead.
       |      root.Effect${listName}CreateWithId(effect.id);
       |    }
       |    public void visit${deleteEffectName}(${deleteEffectName} effect) {
       |      root.Effect${listName}Delete(effect.id);
       |    }
       |    public void visit${addEffectName}(${addEffectName} effect) {
       |      root.Effect${listName}Add(effect.id, effect.index, effect.element);
       |    }
         |    public void visit${removeEffectName}(${removeEffectName} effect) {
         |      root.CheckUnlocked();
         |      root.Effect${listName}RemoveAt(effect.id, effect.index);
         |    }
       """.stripMargin
  }
}
