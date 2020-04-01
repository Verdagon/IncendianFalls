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
           |  List<${elementCSType}> elements;
           |
           |  public ${listCSType}() {
           |    this.elements = new List<${elementCSType}>();
           |  }
           |  public ${listCSType}(params ${elementCSType}[] values) {
           |    this.elements = new List<${elementCSType}>(values);
           |  }
           |  public ${listCSType}(IEnumerable<${elementCSType}> elements) {
           |    this.elements = new List<${elementCSType}>(elements);
           |  }
           |  public int Count { get { return elements.Count; } }
           |
           |  public ${elementCSType} this[int index] { get { return elements[index]; } }
           |
           |  public IEnumerator<${elementCSType}> GetEnumerator() {
           |    return elements.GetEnumerator();
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
           |    foreach (var element in elements) {
           |      result += element.DStr() + ", ";
           |    }
           |    return "(" + result + ")";
           |  }
           |
           |  public int GetDeterministicHashCode() {
           |    int hash = 0;
           |    hash = hash * 37 + elements.Count;
           |    foreach (var element in elements) {
           |      hash = hash * 37 + element.GetDeterministicHashCode();
           |    }
           |    return hash;
           |  }
           |  IEnumerator<${elementCSType}> IEnumerable<${elementCSType}>.GetEnumerator() {
           |    return ((IEnumerable<${elementCSType}>)elements).GetEnumerator();
           |  }
           |  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
           |    return this.GetEnumerator();
           |  }
           |}
         """.stripMargin

    Map(listCSType -> instanceDefinition)
  }

}
