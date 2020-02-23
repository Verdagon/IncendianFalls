using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public class PatternCornerAdjacencyImmList : IEnumerable<PatternCornerAdjacency> {
  List<PatternCornerAdjacency> list;

  public PatternCornerAdjacencyImmList() {
    this.list = new List<PatternCornerAdjacency>();
  }
  public PatternCornerAdjacencyImmList(PatternCornerAdjacency[] list) {
    this.list = new List<PatternCornerAdjacency>(list);
  }
  public PatternCornerAdjacencyImmList(IEnumerable<PatternCornerAdjacency> list) {
    this.list = new List<PatternCornerAdjacency>(list);
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
  IEnumerator<PatternCornerAdjacency> IEnumerable<PatternCornerAdjacency>.GetEnumerator() {
    return ((IEnumerable<PatternCornerAdjacency>)list).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
