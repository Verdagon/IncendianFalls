using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class PatternCornerAdjacencyListList {
  List<PatternCornerAdjacencyList> list;

  public PatternCornerAdjacencyListList() {
    this.list = new List<PatternCornerAdjacencyList>();
  }
  public PatternCornerAdjacencyListList(PatternCornerAdjacencyList[] list) {
    this.list = new List<PatternCornerAdjacencyList>(list);
  }
  public PatternCornerAdjacencyListList(List<PatternCornerAdjacencyList> list) {
    this.list = list;
  }
  public int Count { get { return list.Count; } }

  public PatternCornerAdjacencyList this[int index] { get { return list[index]; } }

  public IEnumerator<PatternCornerAdjacencyList> GetEnumerator() {
    return list.GetEnumerator();
  }

  public int CompareTo(PatternCornerAdjacencyListList that) {
    for (int i = 0; i < Count || i < that.Count; i++) {
      if (i >= Count) {
        return -1;
      }
      if (i >= that.Count) {
        return 1;
      }
      int diff = this[i].CompareTo(that[i]);
      if (diff != 0) {
        return diff;
      }
    }
    return 0;
  }

  public static PatternCornerAdjacencyListList Parse(ParseSource source) {
    throw new Exception("Not implemented!");
  }

  public string DStr() {
    throw new Exception("Not implemented!");
  }

  public int GetDeterministicHashCode() {
    int hash = 0;
    hash = hash * 37 + list.Count;
    foreach (var element in list) {
      hash = hash * 37 + element.GetDeterministicHashCode();
    }
    return hash;
  }
}
         
}
