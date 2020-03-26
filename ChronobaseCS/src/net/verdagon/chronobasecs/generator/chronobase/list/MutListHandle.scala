package net.verdagon.chronobasecs.generator.chronobase.list

import net.verdagon.chronobasecs.compiled.{ImmutableS, ListS, MutableS, OwnS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutListHandle {

  def generateHandle(opt: ChronobaseOptions, list: ListS): Map[String, String] = {
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

    val instanceDefinition =
        s"""
           |public class ${listName} : IEnumerable<${elementCSType}> {
           |  public readonly Root root;
           |  public readonly int id;\n
           |  public ${listName}(Root root, int id) {
           |    this.root = root;
           |    this.id = id;
           |  }
           |  public ${listName}Incarnation incarnation {
           |    get { return root.Get${listName}Incarnation(id); }
           |  }
           |  public void AddObserver(EffectBroadcaster broadcaster, I${listName}EffectObserver observer) {
           |    broadcaster.Add${listName}Observer(id, observer);
           |  }
           |  public void RemoveObserver(EffectBroadcaster broadcaster, I${listName}EffectObserver observer) {
           |    broadcaster.Remove${listName}Observer(id, observer);
           |  }
           |  public void Delete() {
           |    root.Effect${listName}Delete(id);
           |  }
           |  public static ${listName} Null = new ${listName}(null, 0);
           |  public bool Exists() { return root != null && root.${listName}Exists(id); }
           |  public bool NullableIs(${listName} that) {
           |    if (!this.Exists() && !that.Exists()) {
           |      return true;
           |    }
           |    if (!this.Exists() || !that.Exists()) {
           |      return false;
           |    }
           |    return this.Is(that);
           |  }
           |  public bool Is(${listName} that) {
           |    if (!this.Exists()) {
           |      throw new Exception("Called Is on a null!");
           |    }
           |    if (!that.Exists()) {
           |      throw new Exception("Called Is on a null!");
           |    }
           |    return this.root == that.root && id == that.id;
           |  }
           |  public void Add(${elementCSType} element) {
           |""".stripMargin +
        (elementType.kind.mutability match {
          case MutableS => s"    root.Effect${listName}Add(id, Count, element.id);"
          case ImmutableS => s"    root.Effect${listName}Add(id, Count, element);"
        }) +
        s"""
           |  }
           |  public void RemoveAt(int index) {
           |    root.Effect${listName}RemoveAt(id, index);
           |  }
           |  public void AddRange(IEnumerable<${elementCSType}> range) {
           |    foreach (var element in range) {
           |      Add(element);
           |    }
           |  }
           |  public void Clear() {
           |    while (Count > 0) {
           |      RemoveAt(Count - 1);
           |    }
           |  }
           |  public int Count { get { return incarnation.list.Count; } }
           |
           |  public ${elementCSType} this[int index] {
           |""".stripMargin +
        (elementType.kind.mutability match {
          case MutableS => s"    get { return root.Get${elementCSType}(incarnation.list[index]); }\n"
          case ImmutableS => s"    get { return incarnation.list[index]; }\n"
        }) +
        s"""  }
           |  public void Destruct() {
           |    var elements = new List<${elementCSType}>();
           |    foreach (var element in this) {
           |      elements.Add(element);
           |    }
           |    this.Delete();
           |""".stripMargin +
        (elementType.kind.mutability match {
          case ImmutableS => ""
          case MutableS => {
            elementType.ownership match {
              case OwnS => {
                s"""    foreach (var element in elements) {
                   |      element.Destruct();
                   |    }
                   |""".stripMargin
              }
              case _ => ""
            }
          }
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
                     |        violations.Add("Null constraint violated! ${listName}#" + id + "." + element.id);
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
           |  public IEnumerator<${elementCSType}> GetEnumerator() {
           |    foreach (var element in incarnation.list) {
           |""".stripMargin +
        (elementType.kind.mutability match {
          case MutableS => s"      yield return root.Get${elementCSType}(element);"
          case ImmutableS => s"      yield return element;"
        }) +
        s"""
           |    }
           |  }
           |  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
           |    return this.GetEnumerator();
           |  }
           |}
         """.stripMargin

    Map(listName -> instanceDefinition)
  }
}
