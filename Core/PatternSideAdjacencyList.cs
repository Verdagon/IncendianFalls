using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class PatternSideAdjacencyList {
  List<PatternSideAdjacency> list;

  public PatternSideAdjacencyList() {
    this.list = new List<PatternSideAdjacency>();
  }
  public PatternSideAdjacencyList(PatternSideAdjacency[] list) {
    this.list = new List<PatternSideAdjacency>(list);
  }
  public PatternSideAdjacencyList(List<PatternSideAdjacency> list) {
    this.list = list;
  }
  public int Count { get { return list.Count; } }

  public PatternSideAdjacency this[int index] { get { return list[index]; } }

  public IEnumerator<PatternSideAdjacency> GetEnumerator() {
    return list.GetEnumerator();
  }

  public int CompareTo(PatternSideAdjacencyList that) {
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

  public static PatternSideAdjacencyList Parse(ParseSource source) {
    throw new Exception("Not implemented!");
  }

  public string DStr() {
    throw new Exception("Not implemented!");
  }
}
         
}
