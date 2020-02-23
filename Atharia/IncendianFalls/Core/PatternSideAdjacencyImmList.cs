using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class PatternSideAdjacencyImmList : IEnumerable<PatternSideAdjacency> {
  List<PatternSideAdjacency> list;

  public PatternSideAdjacencyImmList() {
    this.list = new List<PatternSideAdjacency>();
  }
  public PatternSideAdjacencyImmList(PatternSideAdjacency[] list) {
    this.list = new List<PatternSideAdjacency>(list);
  }
  public PatternSideAdjacencyImmList(IEnumerable<PatternSideAdjacency> list) {
    this.list = new List<PatternSideAdjacency>(list);
  }
  public int Count { get { return list.Count; } }

  public PatternSideAdjacency this[int index] { get { return list[index]; } }

  public IEnumerator<PatternSideAdjacency> GetEnumerator() {
    return list.GetEnumerator();
  }

  public int CompareTo(PatternSideAdjacencyImmList that) {
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

  public static PatternSideAdjacencyImmList Parse(ParseSource source) {
    throw new Exception("Not implemented!");
  }

  public string DStr() {
    string result = "";
    foreach (var element in list) {
      result += element.DStr() + ", ";
    }
    return "(" + result + ")";
  }

  public int GetDeterministicHashCode() {
    int hash = 0;
    hash = hash * 37 + list.Count;
    foreach (var element in list) {
      hash = hash * 37 + element.GetDeterministicHashCode();
    }
    return hash;
  }
  IEnumerator<PatternSideAdjacency> IEnumerable<PatternSideAdjacency>.GetEnumerator() {
    return ((IEnumerable<PatternSideAdjacency>)list).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
