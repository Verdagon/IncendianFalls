using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class CommTextImmList : IEnumerable<CommText> {
  List<CommText> list;

  public CommTextImmList() {
    this.list = new List<CommText>();
  }
  public CommTextImmList(params CommText[] values) {
    this.list = new List<CommText>(values);
  }
  public CommTextImmList(IEnumerable<CommText> list) {
    this.list = new List<CommText>(list);
  }
  public int Count { get { return list.Count; } }

  public CommText this[int index] { get { return list[index]; } }

  public IEnumerator<CommText> GetEnumerator() {
    return list.GetEnumerator();
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
  IEnumerator<CommText> IEnumerable<CommText>.GetEnumerator() {
    return ((IEnumerable<CommText>)list).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
