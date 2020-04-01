using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class Vec2ImmListImmList : IEnumerable<Vec2ImmList> {
  List<Vec2ImmList> elements;

  public Vec2ImmListImmList() {
    this.elements = new List<Vec2ImmList>();
  }
  public Vec2ImmListImmList(params Vec2ImmList[] values) {
    this.elements = new List<Vec2ImmList>(values);
  }
  public Vec2ImmListImmList(IEnumerable<Vec2ImmList> elements) {
    this.elements = new List<Vec2ImmList>(elements);
  }
  public int Count { get { return elements.Count; } }

  public Vec2ImmList this[int index] { get { return elements[index]; } }

  public IEnumerator<Vec2ImmList> GetEnumerator() {
    return elements.GetEnumerator();
  }

  public int CompareTo(Vec2ImmListImmList that) {
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

  public static Vec2ImmListImmList Parse(ParseSource source) {
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
  IEnumerator<Vec2ImmList> IEnumerable<Vec2ImmList>.GetEnumerator() {
    return ((IEnumerable<Vec2ImmList>)elements).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
