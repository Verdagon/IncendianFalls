using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class PatternTileList {
  List<PatternTile> list;

  public PatternTileList() {
    this.list = new List<PatternTile>();
  }
  public PatternTileList(PatternTile[] list) {
    this.list = new List<PatternTile>(list);
  }
  public PatternTileList(List<PatternTile> list) {
    this.list = list;
  }
  public int Count { get { return list.Count; } }

  public PatternTile this[int index] { get { return list[index]; } }

  public IEnumerator<PatternTile> GetEnumerator() {
    return list.GetEnumerator();
  }

  public int CompareTo(PatternTileList that) {
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

  public static PatternTileList Parse(ParseSource source) {
    throw new Exception("Not implemented!");
  }

  public string DStr() {
    throw new Exception("Not implemented!");
  }
}
         
}
