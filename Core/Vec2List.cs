using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class Vec2List {
  List<Vec2> list;

  public Vec2List() {
    this.list = new List<Vec2>();
  }
  public Vec2List(Vec2[] list) {
    this.list = new List<Vec2>(list);
  }
  public Vec2List(List<Vec2> list) {
    this.list = list;
  }
  public int Count { get { return list.Count; } }

  public Vec2 this[int index] { get { return list[index]; } }

  public IEnumerator<Vec2> GetEnumerator() {
    return list.GetEnumerator();
  }

  public int CompareTo(Vec2List that) {
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

  public static Vec2List Parse(ParseSource source) {
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
}
         
}
