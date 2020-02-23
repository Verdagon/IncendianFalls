package net.verdagon.chronobasecs.generator.chronobase.set

import net.verdagon.chronobasecs.compiled.{ImmutableS, MutableS, OwnS, SetS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutSetHandle {

  def generateMutSet(opt: ChronobaseOptions, set: SetS): Map[String, String] = {
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

    val instanceDefinition =
      s"""public class ${setName} {
         |  public readonly Root root;
         |  public readonly int id;
         |  public ${setName}(Root root, int id) {
         |    this.root = root;
         |    this.id = id;
         |  }
         |  public ${setName}Incarnation incarnation {
         |    get { return root.Get${setName}Incarnation(id); }
         |  }
         |  public void AddObserver(I${setName}EffectObserver observer) {
         |    root.Add${setName}Observer(id, observer);
         |  }
         |  public void RemoveObserver(I${setName}EffectObserver observer) {
         |    root.Remove${setName}Observer(id, observer);
         |  }
         |  public void Add(${elementCSType} element) {
         |    root.Effect${setName}Add(id, element.id);
         |  }
         |  public void Remove(${elementCSType} element) {
         |    root.Effect${setName}Remove(id, element.id);
         |  }
         |  public void Delete() {
         |    root.Effect${setName}Delete(id);
         |  }
         |  public void Clear() {
         |    foreach (var elementId in new List<int>(incarnation.set)) {
         |      root.Effect${setName}Remove(id, elementId);
         |    }
         |  }
         |  public bool Contains(${elementCSType} element) {
         |""".stripMargin +
      (elementType.kind.mutability match {
        case MutableS => s"      return incarnation.set.Contains(element.id);\n"
        case ImmutableS => s"      return incarnation.set.Contains(element);\n"
      }) +
      s"""  }
         |  public int Count { get { return incarnation.set.Count; } }
         |  public IEnumerator<${elementCSType}> GetEnumerator() {
         |    foreach (var element in incarnation.set) {
         |""".stripMargin +
      (elementType.kind.mutability match {
        case MutableS => s"      yield return root.Get${elementCSType}(element);"
        case ImmutableS => s"      yield return element;"
      }) +
      s"""
         |    }
         |  }
         |  public void Destruct() {
         |    var elements = new List<${elementCSType}>();
         |    foreach (var element in this) {
         |      elements.Add(element);
         |    }
         |    this.Delete();
         |""".stripMargin +
      (elementType.ownership match {
        case OwnS => {
          s"""    foreach (var element in elements) {
             |      element.Destruct();
             |    }
             |""".stripMargin
        }
        case _ => ""
      }) +
      s"""  }
         |  public void CheckForNullViolations(List<string> violations) {
         |""".stripMargin +
      (elementType.kind.mutability match {
        case ImmutableS => ""
        case MutableS => {
          s"""    foreach (var element in this) {
             |""".stripMargin +
          (elementType.kind.mutability match {
            case ImmutableS => ""
            case MutableS => {
              val elementCSType = toCS(elementType)
              (if (!elementType.nullable) {
                s"""      if (!root.${elementCSType}Exists(element.id)) {
                   |        violations.Add("Null constraint violated! ${setName}#" + id + "." + element.id);
                   |      }
                   |""".stripMargin
              } else "")
            }
          }) +
          s"""    }
             |""".stripMargin
        }
      }) +
      s"""  }
         |  public void FindReachableObjects(SortedSet<int> foundIds) {
         |    if (foundIds.Contains(id)) {
         |      return;
         |    }
         |    foundIds.Add(id);
         |""".stripMargin +
      (elementType.kind.mutability match {
        case ImmutableS => ""
        case MutableS => {
          s"""    foreach (var element in this) {
             |""".stripMargin +
          (elementType.kind.mutability match {
            case ImmutableS => ""
            case MutableS => {
              val elementCSType = toCS(elementType)
              s"""      if (root.${elementCSType}Exists(element.id)) {
                 |       element.FindReachableObjects(foundIds);
                 |      }
                 |""".stripMargin
            }
          }) +
          s"""    }
             |""".stripMargin
        }
      }) +
      s"""  }
         |}
       """.stripMargin

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
         |  public readonly ${setName}Incarnation incarnation;
         |  public ${createEffectName}(
         |      int id,
         |      ${setName}Incarnation incarnation) {
         |    this.id = id;
         |    this.incarnation = incarnation;
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
      setName -> instanceDefinition,
      observerName -> observerDefinition,
      visitorName -> visitorDefinition,
      createEffectName -> createEffectDefinition,
      deleteEffectName -> deleteEffectDefinition,
      addEffectName -> addEffectDefinition,
      removeEffectName -> removeEffectDefinition)
  }

}
