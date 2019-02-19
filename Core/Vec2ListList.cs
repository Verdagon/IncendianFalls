using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class Vec2ListList {
  List<Vec2List> list;

  public Vec2ListList() {
    this.list = new List<Vec2List>();
  }
  public Vec2ListList(Vec2List[] list) {
    this.list = new List<Vec2List>(list);
  }
  public Vec2ListList(List<Vec2List> list) {
    this.list = list;
  }
  public int Count { get { return list.Count; } }

  public Vec2List this[int index] { get { return list[index]; } }

  public IEnumerator<Vec2List> GetEnumerator() {
    return list.GetEnumerator();
  }

  public int CompareTo(Vec2ListList that) {
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

  public static Vec2ListList Parse(ParseSource source) {
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
