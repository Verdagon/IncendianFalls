using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IntImmList : IEnumerable<int> {
  List<int> elements;

  public IntImmList() {
    this.elements = new List<int>();
  }
  public IntImmList(params int[] values) {
    this.elements = new List<int>(values);
  }
  public IntImmList(IEnumerable<int> elements) {
    this.elements = new List<int>(elements);
  }
  public int Count { get { return elements.Count; } }

  public int this[int index] { get { return elements[index]; } }

  public IEnumerator<int> GetEnumerator() {
    return elements.GetEnumerator();
  }

  public int CompareTo(IntImmList that) {
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

  public static IntImmList Parse(ParseSource source) {
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
  IEnumerator<int> IEnumerable<int>.GetEnumerator() {
    return ((IEnumerable<int>)elements).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
