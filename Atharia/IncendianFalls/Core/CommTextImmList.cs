using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CommTextImmList : IEnumerable<CommText> {
  List<CommText> elements;

  public CommTextImmList() {
    this.elements = new List<CommText>();
  }
  public CommTextImmList(params CommText[] values) {
    this.elements = new List<CommText>(values);
  }
  public CommTextImmList(IEnumerable<CommText> elements) {
    this.elements = new List<CommText>(elements);
  }
  public int Count { get { return elements.Count; } }

  public CommText this[int index] { get { return elements[index]; } }

  public IEnumerator<CommText> GetEnumerator() {
    return elements.GetEnumerator();
  }

  public int CompareTo(CommTextImmList that) {
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

  public static CommTextImmList Parse(ParseSource source) {
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
  IEnumerator<CommText> IEnumerable<CommText>.GetEnumerator() {
    return ((IEnumerable<CommText>)elements).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
