using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TileExplodingEvent : IComparable<TileExplodingEvent> {
  public static readonly string NAME = "TileExplodingEvent";
  public class EqualityComparer : IEqualityComparer<TileExplodingEvent> {
    public bool Equals(TileExplodingEvent a, TileExplodingEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(TileExplodingEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<TileExplodingEvent> {
    public int Compare(TileExplodingEvent a, TileExplodingEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public readonly Location loc;
  public TileExplodingEvent(
      int time,
      Location loc) {
    this.time = time;
    this.loc = loc;
    int hash = 0;
    hash = hash * 37 + time.GetDeterministicHashCode();
    hash = hash * 37 + loc.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(TileExplodingEvent a, TileExplodingEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(TileExplodingEvent a, TileExplodingEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is TileExplodingEvent)) {
      return false;
    }
    var that = obj as TileExplodingEvent;
    return true
               && time.Equals(that.time)
        && loc.Equals(that.loc)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(TileExplodingEvent that) {
    if (time != that.time) {
      return time.CompareTo(that.time);
    }
    if (loc != that.loc) {
      return loc.CompareTo(that.loc);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "TileExplodingEvent(" +
        time.DStr() + ", " +
        loc.DStr()
        + ")";

    }
    public static TileExplodingEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(",");
      var loc = Location.Parse(source);
      source.Expect(")");
      return new TileExplodingEvent(time, loc);
  }
}
       
}
