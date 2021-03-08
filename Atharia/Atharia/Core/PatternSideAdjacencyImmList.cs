using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class PatternSideAdjacencyImmList : IEnumerable<PatternSideAdjacency> {
  List<PatternSideAdjacency> elements;

  public PatternSideAdjacencyImmList() {
    this.elements = new List<PatternSideAdjacency>();
  }
  public PatternSideAdjacencyImmList(params PatternSideAdjacency[] values) {
    this.elements = new List<PatternSideAdjacency>(values);
  }
  public PatternSideAdjacencyImmList(IEnumerable<PatternSideAdjacency> elements) {
    this.elements = new List<PatternSideAdjacency>(elements);
  }
  public int Count { get { return elements.Count; } }

  public PatternSideAdjacency this[int index] { get { return elements[index]; } }

  public IEnumerator<PatternSideAdjacency> GetEnumerator() {
    return elements.GetEnumerator();
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
    foreach (var element in elements) {
      result += element.DStr() + ", ";
    }
    return "(" + result + ")";
  }

  public int GetDeterministicHashCode() {
    int hash = 0;
    hash = hash * 37 + elements.Count;
    foreach (var element in elements) {
      hash = hash * 37 + element.GetDeterministicHashCode();
    }
    return hash;
  }
  IEnumerator<PatternSideAdjacency> IEnumerable<PatternSideAdjacency>.GetEnumerator() {
    return ((IEnumerable<PatternSideAdjacency>)elements).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
