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
    val flattenedElementCSType = toCS(elementType.flatten)

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
         |  public void AddObserver(EffectBroadcaster broadcaster, I${setName}EffectObserver observer) {
         |    broadcaster.Add${setName}Observer(id, observer);
         |  }
         |  public void RemoveObserver(EffectBroadcaster broadcaster, I${setName}EffectObserver observer) {
         |    broadcaster.Remove${setName}Observer(id, observer);
         |  }
         |  public void Add(${elementCSType} element) {
         |  """.stripMargin +
      (elementType.kind.mutability match {
        case MutableS => s"    root.Effect${setName}Add(id, element.id);"
        case ImmutableS => s"    root.Effect${setName}Add(id, element);"
      }) +
      s"""
         |  }
         |  public void Remove(${elementCSType} element) {
         |  """.stripMargin +
      (elementType.kind.mutability match {
        case MutableS => s"    root.Effect${setName}Remove(id, element.id);"
        case ImmutableS => s"    root.Effect${setName}Remove(id, element);"
      }) +
      s"""
         |  }
         |  public void Delete() {
         |    root.Effect${setName}Delete(id);
         |  }
         |  public void Clear() {
         |    foreach (var element in new List<${flattenedElementCSType}>(incarnation.set)) {
         |      root.Effect${setName}Remove(id, element);
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

    Map(
      setName -> instanceDefinition)
  }

}
