package net.verdagon.chronobasecs.generator

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutStructEffects {

  def generateVisitorMethods(struct: StructS): String = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct
    val createEffectName = s"${structName}CreateEffect";
    val deleteEffectName = s"${structName}DeleteEffect";

    s"  void visit${createEffectName}(${createEffectName} effect);\n" +
      s"  void visit${deleteEffectName}(${deleteEffectName} effect);\n" +
      members.map({
        case StructMemberS(_, FinalS, _) => ""
        case StructMemberS(memberName, VaryingS, _) => {
          val effectName = s"${structName}Set${memberName.capitalize}Effect"
          s"  void visit${effectName}(${effectName} effect);\n"
        }
      }).mkString("")
  }

  def generateEffects(opt: ChronobaseOptions, struct: StructS): Map[String, String] = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct

    val incarnationName = s"${structName}Incarnation";
    val ieffectName = s"I${structName}Effect";
    val observerName = s"I${structName}EffectObserver"
    val visitorName = s"I${structName}EffectVisitor";
    val createEffectName = s"${structName}CreateEffect";
    val deleteEffectName = s"${structName}DeleteEffect";

    val ieffectDefinition =
      s"""
         |public interface ${ieffectName} : IEffect {
         |  int id { get; }
         |  void visit${ieffectName}(${visitorName} visitor);
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
        generateVisitorMethods(struct) +
        s"}\n"

    val createEffectDefinition =
      s"""
         |public struct ${createEffectName} : ${ieffectName} {
         |  public readonly int id;
         |  public readonly ${incarnationName} incarnation;
         |  public ${createEffectName}(int id, ${incarnationName} incarnation) {
         |    this.id = id;
         |    this.incarnation = incarnation;
         |  }
         |  int ${ieffectName}.id => id;
         |  public void visit${ieffectName}(${visitorName} visitor) {
         |    visitor.visit${createEffectName}(this);
         |  }
         |  public void visitIEffect(IEffectVisitor visitor) {
         |    visitor.visit${structName}Effect(this);
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
         |    visitor.visit${structName}Effect(this);
         |  }
         |  public bool isSubtractive() { return true; }
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
               |  public readonly ${toCS(memberType.flatten)} newValue;
               |  public ${effectName}(
               |      int id,
               |      ${toCS(memberType.flatten)} newValue) {
               |    this.id = id;
               |    this.newValue = newValue;
               |  }
               |  int ${ieffectName}.id => id;
               |
               |  public void visit${ieffectName}(${visitorName} visitor) {
               |    visitor.visit${effectName}(this);
               |  }
               |  public void visitIEffect(IEffectVisitor visitor) {
               |    visitor.visit${structName}Effect(this);
               |  }
               |  public bool isSubtractive() { return false; }
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


  def generateGlobalVisitorInterfaceMethods(struct: StructS) = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct

    val ieffectName = s"I${structName}Effect"

    s"void visit${structName}Effect(${ieffectName} effect);\n"
  }
  def generateEffectBroadcasterMembers(struct: StructS): String = {
    val structCSType = toCS(struct.tyype)
    s"""
       |  readonly SortedDictionary<int, List<I${structCSType}EffectObserver>> observersFor${structCSType} =
       |      new SortedDictionary<int, List<I${structCSType}EffectObserver>>();
       |""".stripMargin
  }

  def generateRootApplierMethods(struct: StructS) = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct

    val structCSType = toCS(struct.tyype)
    val ieffectName = s"I${structName}Effect"

    s"""
       |  public void visit${structName}Effect(${ieffectName} effect) {
       |    if (observersFor${structCSType}.TryGetValue(effect.id, out var observers)) {
       |      foreach (var observer in new List<I${structCSType}EffectObserver>(observers)) {
       |        observer.On${structName}Effect(effect);
       |      }
       |    }
       |  }
       |""".stripMargin
  }

  def generateEffectBroadcasterMethods(struct: StructS) = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct

    val structCSType = toCS(struct.tyype)
    val ieffectName = s"I${structName}Effect"

    s"""
       |  public void visit${structName}Effect(${ieffectName} effect) {
       |    if (observersFor${structCSType}.TryGetValue(effect.id, out var observers)) {
       |      foreach (var observer in new List<I${structCSType}EffectObserver>(observers)) {
       |        observer.On${structName}Effect(effect);
       |      }
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
       |""".stripMargin
  }
  def generateEffectApplierMethods(struct: StructS): String = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct
    s"""
       |public void visit${structName}Effect(I${structName}Effect effect) { effect.visitI${structName}Effect(this); }
       |""".stripMargin +
    generateEffectApplierCreateMethod(struct) +
      generateEffectApplierDeleteMethod(struct) +
      members.zipWithIndex.map({
        case (StructMemberS(memberName, FinalS, memberType), memberIndex) => ""
        case (StructMemberS(memberName, VaryingS, memberType), memberIndex) =>
          generateEffectApplierSetterMethod(struct, memberIndex, memberName, memberType)
      }).mkString("")
  }


  def generateEffectApplierCreateMethod(struct: StructS): String = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct
    val createEffect = s"${structName}CreateEffect"
    s"  public void visit${createEffect}(${createEffect} effect) {\n" +
      s"    // For now we're just feeding the remote ID in. Someday we might want to have a map\n" +
        s"    // in the applier instead.\n" +
      s"    root.TrustedEffect${structName}CreateWithId(effect.id\n" +
      // We have to pass in the effect contents directly, we cant GetXYZ() them first because
      // if this effect came from a revert, then we're doing a bunch of EffectXYZCreate in a
      // crazy non-dependency-order, and the GetXYZ() would fail.
      // We cant directly make the handles here either, because you cant directly make a interface
      // handle.
      members.map({ case StructMemberS(memberName, variability, memberType) =>
        s",  effect.incarnation.${memberName}"
      }).mkString("\n") +
      s"    );\n" +
      s"""
        |}
        |""".stripMargin
  }
  def generateEffectApplierDeleteMethod(
                                              struct: StructS
                                            ): String = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct
    val deleteEffect = s"${structName}DeleteEffect"
    s"""
       |  public void visit${deleteEffect}(${deleteEffect} effect) {
       |    root.Effect${structName}Delete(effect.id);
       |  }
       |
     """.stripMargin
  }

  def generateEffectApplierSetterMethod(
                                              struct: StructS,
                                              memberIndex: Int,
                                              memberName: String,
                                              memberType: TypeS[IKindS]): String = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct
    val setEffectName = s"${struct.name}Set${memberName.capitalize}Effect"
    s"""
       |  public void visit${setEffectName}(${setEffectName} effect) {
       |    root.Effect${struct.name}Set${memberName.capitalize}(
       |      effect.id,
       |""".stripMargin +
      (memberType.kind.mutability match {
        case MutableS => {
          if (memberType.ownership == WeakS || memberType.nullable) {
            s"  root.Get${memberType.name}OrNull(effect.newValue)"
          } else {
            s"  root.Get${memberType.name}(effect.newValue)"
          }
        }
        case ImmutableS => s"  effect.newValue"
      }) +
    s"""
       |    );
       |  }
       |""".stripMargin
  }
}
