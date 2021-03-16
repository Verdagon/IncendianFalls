using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TileBurningEvent : IComparable<TileBurningEvent> {
  public static readonly string NAME = "TileBurningEvent";
  public class EqualityComparer : IEqualityComparer<TileBurningEvent> {
    public bool Equals(TileBurningEvent a, TileBurningEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(TileBurningEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<TileBurningEvent> {
    public int Compare(TileBurningEvent a, TileBurningEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public readonly Location loc;
  public TileBurningEvent(
      int time,
      Location loc) {
    this.time = time;
    this.loc = loc;
    int hash = 0;
    hash = hash * 37 + time.GetDeterministicHashCode();
    hash = hash * 37 + loc.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(TileBurningEvent a, TileBurningEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(TileBurningEvent a, TileBurningEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is TileBurningEvent)) {
      return false;
    }
    var that = obj as TileBurningEvent;
    return true
               && time.Equals(that.time)
        && loc.Equals(that.loc)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(TileBurningEvent that) {
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
    return "TileBurningEvent(" +
        time.DStr() + ", " +
        loc.DStr()
        + ")";

    }
    public static TileBurningEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(",");
      var loc = Location.Parse(source);
      source.Expect(")");
      return new TileBurningEvent(time, loc);
  }
}
       
}
