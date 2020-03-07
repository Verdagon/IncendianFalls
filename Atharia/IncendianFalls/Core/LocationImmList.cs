using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class LocationImmList : IEnumerable<Location> {
  List<Location> list;

  public LocationImmList() {
    this.list = new List<Location>();
  }
  public LocationImmList(Location[] list) {
    this.list = new List<Location>(list);
  }
  public LocationImmList(IEnumerable<Location> list) {
    this.list = new List<Location>(list);
  }
  public int Count { get { return list.Count; } }

  public Location this[int index] { get { return list[index]; } }

  public IEnumerator<Location> GetEnumerator() {
    return list.GetEnumerator();
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
    foreach (var element in list) {
      result += element.DStr() + ", ";
    }
    return "(" + result + ")";
  }

  public int GetDeterministicHashCode() {
    int hash = 0;
    hash = hash * 37 + list.Count;
    foreach (var element in list) {
      hash = hash * 37 + element.GetDeterministicHashCode();
    }
    return hash;
  }
  IEnumerator<Location> IEnumerable<Location>.GetEnumerator() {
    return ((IEnumerable<Location>)list).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
