using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class PatternCornerAdjacencyImmList : IEnumerable<PatternCornerAdjacency> {
  List<PatternCornerAdjacency> elements;

  public PatternCornerAdjacencyImmList() {
    this.elements = new List<PatternCornerAdjacency>();
  }
  public PatternCornerAdjacencyImmList(params PatternCornerAdjacency[] values) {
    this.elements = new List<PatternCornerAdjacency>(values);
  }
  public PatternCornerAdjacencyImmList(IEnumerable<PatternCornerAdjacency> elements) {
    this.elements = new List<PatternCornerAdjacency>(elements);
  }
  public int Count { get { return elements.Count; } }

  public PatternCornerAdjacency this[int index] { get { return elements[index]; } }

  public IEnumerator<PatternCornerAdjacency> GetEnumerator() {
    return elements.GetEnumerator();
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
  IEnumerator<PatternCornerAdjacency> IEnumerable<PatternCornerAdjacency>.GetEnumerator() {
    return ((IEnumerable<PatternCornerAdjacency>)elements).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
