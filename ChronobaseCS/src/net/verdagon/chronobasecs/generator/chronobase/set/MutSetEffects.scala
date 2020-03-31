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
      s"""public interface ${ieffectName} : IEffect {
         |  int id { get; }
         |  void visit${ieffectName}(${visitorName} visitor);
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
         |  public void visit${ieffectName}(${visitorName} visitor) {
         |    visitor.visit${createEffectName}(this);
         |  }
         |  public void visitIEffect(IEffectVisitor visitor) {
         |    visitor.visit${setName}Effect(this);
         |  }
         |  public bool isSubtractive() { return false; }
         |}
         |""".stripMargin

    val deleteEffectDefinition =
      s"""public struct ${deleteEffectName} : ${ieffectName} {
         |  public readonly int id;
         |  public ${deleteEffectName}(int id) {
         |    this.id = id;
         |  }
         |  int ${ieffectName}.id => id;
         |  public void visit${ieffectName}(${visitorName} visitor) {
         |    visitor.visit${deleteEffectName}(this);
         |  }
         |  public void visitIEffect(IEffectVisitor visitor) {
         |    visitor.visit${setName}Effect(this);
         |  }
         |  public bool isSubtractive() { return true; }
         |}
         |""".stripMargin

    val addEffectDefinition =
      s"""public struct ${addEffectName} : ${ieffectName} {
         |  public readonly int id;
         |  public readonly int element;
         |  public ${addEffectName}(int id, int element) {
         |    this.id = id;
         |    this.element = element;
         |  }
         |  int ${ieffectName}.id => id;
         |  public void visit${ieffectName}(${visitorName} visitor) {
         |    visitor.visit${addEffectName}(this);
         |  }
         |  public void visitIEffect(IEffectVisitor visitor) {
         |    visitor.visit${setName}Effect(this);
         |  }
         |  public bool isSubtractive() { return false; }
         |}
         |""".stripMargin

    val removeEffectDefinition =
      s"""public struct ${removeEffectName} : ${ieffectName} {
         |  public readonly int id;
         |  public readonly int element;
         |  public ${removeEffectName}(int id, int element) {
         |    this.id = id;
         |    this.element = element;
         |  }
         |  int ${ieffectName}.id => id;
         |  public void visit${ieffectName}(${visitorName} visitor) {
         |    visitor.visit${removeEffectName}(this);
         |  }
         |  public void visitIEffect(IEffectVisitor visitor) {
         |    visitor.visit${setName}Effect(this);
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

  def generateGlobalVisitorInterfaceMethods(set: SetS) = {
    val SetS(setName, MutableS, elementType) = set

    val ieffectName = s"I${setName}Effect"

    s"void visit${setName}Effect(${ieffectName} effect);\n"
  }

  def generateEffectBroadcasterMethods(set: SetS) = {
    val SetS(setName, MutableS, elementType) = set

    val ieffectName = s"I${setName}Effect"
    val setCSType = toCS(set.tyype)

    s"""
       |    public void visit${setName}Effect(${ieffectName} effect) {
       |      if (observersFor${setCSType}.TryGetValue(effect.id, out var observers)) {
       |        foreach (var observer in new List<I${setCSType}EffectObserver>(observers)) {
       |          observer.On${setCSType}Effect(effect);
       |        }
       |      }
       |    }
       |    public void Add${setName}Observer(int id, I${setName}EffectObserver observer) {
       |      List<I${setName}EffectObserver> obsies;
       |      if (!observersFor${setName}.TryGetValue(id, out obsies)) {
       |        obsies = new List<I${setName}EffectObserver>();
       |      }
       |      obsies.Add(observer);
       |      observersFor${setName}[id] = obsies;
       |    }
       |    public void Remove${setName}Observer(int id, I${setName}EffectObserver observer) {
       |      if (observersFor${setName}.ContainsKey(id)) {
       |        var list = observersFor${setName}[id];
       |        list.Remove(observer);
       |        if (list.Count == 0) {
       |          observersFor${setName}.Remove(id);
       |        }
       |      } else {
       |        throw new Exception("Couldnt find!");
       |      }
       |    }
       |""".stripMargin
  }
  def generateEffectApplierMethods(set: SetS): String = {
    val SetS(setName, MutableS, elementType) = set

    val createEffectName = s"${setName}CreateEffect"
    val deleteEffectName = s"${setName}DeleteEffect"
    val addEffectName = s"${setName}AddEffect"
    val removeEffectName = s"${setName}RemoveEffect"
    val elementCSType = toCS(elementType)

    s"""
       |    public void visit${setName}Effect(I${setName}Effect effect) { effect.visitI${setName}Effect(this); }
       |    public void visit${createEffectName}(${createEffectName} effect) {
       |      // For now we're just feeding the remote ID in. Someday we might want to have a map
       |      // in the applier instead.
       |      root.TrustedEffect${setName}CreateWithId(effect.id);
       |    }
       |    public void visit${deleteEffectName}(${deleteEffectName} effect) {
       |      root.Effect${setName}Delete(effect.id);
       |    }
       |    public void visit${addEffectName}(${addEffectName} effect) {
        |     root.Effect${setName}Add(effect.id, effect.element);
        | }
         |    public void visit${removeEffectName}(${removeEffectName} effect) {
         |      root.CheckUnlocked();
         |      root.Effect${setName}Remove(effect.id, effect.element);
         |    }
       """.stripMargin
  }
}
