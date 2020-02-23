package net.verdagon.chronobasecs.generator.chronobase.list

import net.verdagon.chronobasecs.compiled.{ImmutableS, ListS, MutabilityS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object ImmList {

  def generateImmList(
                       opt: ChronobaseOptions,
                       list: ListS
  ): Map[String, String] = {
    val ListS(listName, ImmutableS, elementType) = list

    val listCSType = toCS(list.tyype)
    val elementCSType = toCS(elementType)

    val instanceDefinition =
        s"""
           |public class ${listCSType} : IEnumerable<${elementCSType}> {
           |  List<${elementCSType}> list;
           |
           |  public ${listCSType}() {
           |    this.list = new List<${elementCSType}>();
           |  }
           |  public ${listCSType}(${elementCSType}[] list) {
           |    this.list = new List<${elementCSType}>(list);
           |  }
           |  public ${listCSType}(IEnumerable<${elementCSType}> list) {
           |    this.list = new List<${elementCSType}>(list);
           |  }
           |  public int Count { get { return list.Count; } }
           |
           |  public ${elementCSType} this[int index] { get { return list[index]; } }
           |
           |  public IEnumerator<${elementCSType}> GetEnumerator() {
           |    return list.GetEnumerator();
           |  }
           |
           |  public int CompareTo(${listCSType} that) {
           |    for (int i = 0; i < Count || i < that.Count; i++) {
           |      if (i >= Count) {
           |        return -1;
           |      }
           |      if (i >= that.Count) {
           |        return 1;
           |      }
           |      int diff = this[i].CompareTo(that[i]);
           |      if (diff != 0) {
           |        return diff;
           |      }
           |    }
           |    return 0;
           |  }
           |
           |  public static ${listCSType} Parse(ParseSource source) {
           |    throw new Exception("Not implemented!");
           |  }
           |
           |  public string DStr() {
           |    string result = "";
           |    foreach (var element in list) {
           |      result += element.DStr() + ", ";
           |    }
           |    return "(" + result + ")";
           |  }
           |
           |  public int GetDeterministicHashCode() {
           |    int hash = 0;
           |    hash = hash * 37 + list.Count;
           |    foreach (var element in list) {
           |      hash = hash * 37 + element.GetDeterministicHashCode();
           |    }
           |    return hash;
           |  }
           |  IEnumerator<${elementCSType}> IEnumerable<${elementCSType}>.GetEnumerator() {
           |    return ((IEnumerable<${elementCSType}>)list).GetEnumerator();
           |  }
           |  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
           |    return this.GetEnumerator();
           |  }
           |}
         """.stripMargin

    Map(listCSType -> instanceDefinition)
  }

}
