using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CommActionImmList : IEnumerable<CommAction> {
  List<CommAction> elements;

  public CommActionImmList() {
    this.elements = new List<CommAction>();
  }
  public CommActionImmList(params CommAction[] values) {
    this.elements = new List<CommAction>(values);
  }
  public CommActionImmList(IEnumerable<CommAction> elements) {
    this.elements = new List<CommAction>(elements);
  }
  public int Count { get { return elements.Count; } }

  public CommAction this[int index] { get { return elements[index]; } }

  public IEnumerator<CommAction> GetEnumerator() {
    return elements.GetEnumerator();
  }

  public int CompareTo(CommActionImmList that) {
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

  public static CommActionImmList Parse(ParseSource source) {
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
  IEnumerator<CommAction> IEnumerable<CommAction>.GetEnumerator() {
    return ((IEnumerable<CommAction>)elements).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
