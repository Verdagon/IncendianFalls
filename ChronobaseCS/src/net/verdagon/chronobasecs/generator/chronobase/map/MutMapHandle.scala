package net.verdagon.chronobasecs.generator.chronobase.map

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutMapHandle {
  def generateHandle(
                      opt: ChronobaseOptions,
                      map: MapS
  ): Map[String, String] = {
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

    val instanceDefinition =
        s"""
           |public class ${mapName} {
           |  public readonly Root root;
           |  public readonly int id;\n
           |
           |  public ${mapName}(Root root, int id) {
           |    this.root = root;
           |    this.id = id;
           |  }
           |
           |  public ${mapName}Incarnation incarnation {
           |    get { return root.Get${mapName}Incarnation(id); }
           |  }
           |
           |  public bool Exists() { return root != null && root.${mapName}Exists(id); }
           |
           |  public void AddObserver(EffectBroadcaster broadcaster, I${mapName}EffectObserver observer) {
           |    broadcaster.Add${mapName}Observer(id, observer);
           |  }
           |  public void RemoveObserver(EffectBroadcaster broadcaster, I${mapName}EffectObserver observer) {
           |    broadcaster.Remove${mapName}Observer(id, observer);
           |  }
           |
           |  public void Add(${keyCSType} key, ${elementCSType} value) {
           |  """.stripMargin +
        (elementType.kind.mutability match {
          case MutableS => s"    root.Effect${mapName}Add(id, key, value.id);"
          case ImmutableS => s"    root.Effect${mapName}Add(id, key, value);"
        }) +
        s"""
           |  }
           |
           |  public void Remove(${keyCSType} key) {
           |    root.Effect${mapName}Remove(id, key);
           |  }
           |
           |  public int Count { get { return incarnation.elements.Count; } }
           |
           |  public void CheckForNullViolations(List<string> violations) {
           |""".stripMargin +
        (elementType.kind.mutability match {
          case ImmutableS => ""
          case MutableS => {
            s"""    foreach (var entry in this) {
               |      var element = entry.Value;
               |""".stripMargin +
            (elementType.kind.mutability match {
              case ImmutableS => ""
              case MutableS => {
                val elementCSType = toCS(elementType)
                (if (!elementType.nullable) {
                  s"""      if (!root.${elementCSType}Exists(element.id)) {
                     |        violations.Add("Null constraint violated! ${mapName}#" + id + "." + element.id);
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
           |
           |  public void Delete() {
           |    root.Effect${mapName}Delete(id);
           |  }
           |  public void Destruct() {
           |    var elements = new List<${elementCSType}>();
           |    foreach (var entry in this) {
           |      elements.Add(entry.Value);
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
           |  public void FindReachableObjects(SortedSet<int> foundIds) {
           |    if (foundIds.Contains(id)) {
           |      return;
           |    }
           |    foundIds.Add(id);
           |""".stripMargin +
        (elementType.kind.mutability match {
          case ImmutableS => ""
          case MutableS => {
            s"""    foreach (var entry in this) {
               |      var element = entry.Value;
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
           |  public bool ContainsKey(${keyCSType} key) {
           |    return incarnation.elements.ContainsKey(key);
           |  }
           |
           |  public List<Location> Keys {
           |    // Could be optimized, I think SortedDictionary uses an inner class instead of making a List
           |    // like this.
           |    get { return new List<Location>(incarnation.elements.Keys); }
           |  }
           |
           |  public ${elementCSType} this[${keyCSType} key] {
           |""".stripMargin +
        (elementType.kind.mutability match {
          case MutableS => s"    get { return new ${elementCSType}(root, incarnation.elements[key]); }"
          case ImmutableS => s"    get { return incarnation.elements[key]; }"
        }) +
        s"""
           |  }
           |
           |  public IEnumerator<KeyValuePair<${keyCSType}, ${elementCSType}>> GetEnumerator() {
           |    foreach (var keyAndValue in incarnation.elements) {
           |      yield return new KeyValuePair<${keyCSType}, ${elementCSType}>(
           |          keyAndValue.Key,
           |          new ${elementCSType}(root, keyAndValue.Value));
           |    }
           |  }
           |}
         """.stripMargin

    Map(mapName -> instanceDefinition)
  }

}
