using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class PatternTileImmList : IEnumerable<PatternTile> {
  List<PatternTile> elements;

  public PatternTileImmList() {
    this.elements = new List<PatternTile>();
  }
  public PatternTileImmList(params PatternTile[] values) {
    this.elements = new List<PatternTile>(values);
  }
  public PatternTileImmList(IEnumerable<PatternTile> elements) {
    this.elements = new List<PatternTile>(elements);
  }
  public int Count { get { return elements.Count; } }

  public PatternTile this[int index] { get { return elements[index]; } }

  public IEnumerator<PatternTile> GetEnumerator() {
    return elements.GetEnumerator();
  }

  public int CompareTo(PatternTileImmList that) {
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

  public static PatternTileImmList Parse(ParseSource source) {
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
  IEnumerator<PatternTile> IEnumerable<PatternTile>.GetEnumerator() {
    return ((IEnumerable<PatternTile>)elements).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
