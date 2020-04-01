using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LocationImmList : IEnumerable<Location> {
  List<Location> elements;

  public LocationImmList() {
    this.elements = new List<Location>();
  }
  public LocationImmList(params Location[] values) {
    this.elements = new List<Location>(values);
  }
  public LocationImmList(IEnumerable<Location> elements) {
    this.elements = new List<Location>(elements);
  }
  public int Count { get { return elements.Count; } }

  public Location this[int index] { get { return elements[index]; } }

  public IEnumerator<Location> GetEnumerator() {
    return elements.GetEnumerator();
  }

  public int CompareTo(LocationImmList that) {
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

  public static LocationImmList Parse(ParseSource source) {
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
  IEnumerator<Location> IEnumerable<Location>.GetEnumerator() {
    return ((IEnumerable<Location>)elements).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
