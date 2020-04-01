using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class PatternCornerAdjacencyImmListImmList : IEnumerable<PatternCornerAdjacencyImmList> {
  List<PatternCornerAdjacencyImmList> elements;

  public PatternCornerAdjacencyImmListImmList() {
    this.elements = new List<PatternCornerAdjacencyImmList>();
  }
  public PatternCornerAdjacencyImmListImmList(params PatternCornerAdjacencyImmList[] values) {
    this.elements = new List<PatternCornerAdjacencyImmList>(values);
  }
  public PatternCornerAdjacencyImmListImmList(IEnumerable<PatternCornerAdjacencyImmList> elements) {
    this.elements = new List<PatternCornerAdjacencyImmList>(elements);
  }
  public int Count { get { return elements.Count; } }

  public PatternCornerAdjacencyImmList this[int index] { get { return elements[index]; } }

  public IEnumerator<PatternCornerAdjacencyImmList> GetEnumerator() {
    return elements.GetEnumerator();
  }

  public int CompareTo(PatternCornerAdjacencyImmListImmList that) {
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

  public static PatternCornerAdjacencyImmListImmList Parse(ParseSource source) {
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
  IEnumerator<PatternCornerAdjacencyImmList> IEnumerable<PatternCornerAdjacencyImmList>.GetEnumerator() {
    return ((IEnumerable<PatternCornerAdjacencyImmList>)elements).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
