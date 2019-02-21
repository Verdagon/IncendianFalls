using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class PatternTileImmList {
  List<PatternTile> list;

  public PatternTileImmList() {
    this.list = new List<PatternTile>();
  }
  public PatternTileImmList(PatternTile[] list) {
    this.list = new List<PatternTile>(list);
  }
  public PatternTileImmList(List<PatternTile> list) {
    this.list = list;
  }
  public int Count { get { return list.Count; } }

  public PatternTile this[int index] { get { return list[index]; } }

  public IEnumerator<PatternTile> GetEnumerator() {
    return list.GetEnumerator();
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
