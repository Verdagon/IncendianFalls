using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class Vec2ImmListImmList : IEnumerable<Vec2ImmList> {
  List<Vec2ImmList> list;

  public Vec2ImmListImmList() {
    this.list = new List<Vec2ImmList>();
  }
  public Vec2ImmListImmList(Vec2ImmList[] list) {
    this.list = new List<Vec2ImmList>(list);
  }
  public Vec2ImmListImmList(IEnumerable<Vec2ImmList> list) {
    this.list = new List<Vec2ImmList>(list);
  }
  public int Count { get { return list.Count; } }

  public Vec2ImmList this[int index] { get { return list[index]; } }

  public IEnumerator<Vec2ImmList> GetEnumerator() {
    return list.GetEnumerator();
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
    throw new Exception("Not implemented!");
  }

  public int GetDeterministicHashCode() {
    int hash = 0;
    hash = hash * 37 + list.Count;
    foreach (var element in list) {
      hash = hash * 37 + element.GetDeterministicHashCode();
    }
    return hash;
  }
  IEnumerator<Vec2ImmList> IEnumerable<Vec2ImmList>.GetEnumerator() {
    return ((IEnumerable<Vec2ImmList>)list).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
