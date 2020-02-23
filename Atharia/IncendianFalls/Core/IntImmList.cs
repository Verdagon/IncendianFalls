using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class IntImmList : IEnumerable<int> {
  List<int> list;

  public IntImmList() {
    this.list = new List<int>();
  }
  public IntImmList(int[] list) {
    this.list = new List<int>(list);
  }
  public IntImmList(IEnumerable<int> list) {
    this.list = new List<int>(list);
  }
  public int Count { get { return list.Count; } }

  public int this[int index] { get { return list[index]; } }

  public IEnumerator<int> GetEnumerator() {
    return list.GetEnumerator();
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
  IEnumerator<int> IEnumerable<int>.GetEnumerator() {
    return ((IEnumerable<int>)list).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
