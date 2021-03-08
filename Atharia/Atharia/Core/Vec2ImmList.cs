using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class Vec2ImmList : IEnumerable<Vec2> {
  List<Vec2> elements;

  public Vec2ImmList() {
    this.elements = new List<Vec2>();
  }
  public Vec2ImmList(params Vec2[] values) {
    this.elements = new List<Vec2>(values);
  }
  public Vec2ImmList(IEnumerable<Vec2> elements) {
    this.elements = new List<Vec2>(elements);
  }
  public int Count { get { return elements.Count; } }

  public Vec2 this[int index] { get { return elements[index]; } }

  public IEnumerator<Vec2> GetEnumerator() {
    return elements.GetEnumerator();
  }

  public int CompareTo(Vec2ImmList that) {
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

  public static Vec2ImmList Parse(ParseSource source) {
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
  IEnumerator<Vec2> IEnumerable<Vec2>.GetEnumerator() {
    return ((IEnumerable<Vec2>)elements).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
