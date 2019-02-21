using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class PatternCornerAdjacencyImmList {
  List<PatternCornerAdjacency> list;

  public PatternCornerAdjacencyImmList() {
    this.list = new List<PatternCornerAdjacency>();
  }
  public PatternCornerAdjacencyImmList(PatternCornerAdjacency[] list) {
    this.list = new List<PatternCornerAdjacency>(list);
  }
  public PatternCornerAdjacencyImmList(List<PatternCornerAdjacency> list) {
    this.list = list;
  }
  public int Count { get { return list.Count; } }

  public PatternCornerAdjacency this[int index] { get { return list[index]; } }

  public IEnumerator<PatternCornerAdjacency> GetEnumerator() {
    return list.GetEnumerator();
  }

  public int CompareTo(PatternCornerAdjacencyImmList that) {
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

  public static PatternCornerAdjacencyImmList Parse(ParseSource source) {
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
