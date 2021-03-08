using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitCounteringEvent : IComparable<UnitCounteringEvent> {
  public static readonly string NAME = "UnitCounteringEvent";
  public class EqualityComparer : IEqualityComparer<UnitCounteringEvent> {
    public bool Equals(UnitCounteringEvent a, UnitCounteringEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(UnitCounteringEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<UnitCounteringEvent> {
    public int Compare(UnitCounteringEvent a, UnitCounteringEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public UnitCounteringEvent(
      int time) {
    this.time = time;
    int hash = 0;
    hash = hash * 37 + time.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(UnitCounteringEvent a, UnitCounteringEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(UnitCounteringEvent a, UnitCounteringEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is UnitCounteringEvent)) {
      return false;
    }
    var that = obj as UnitCounteringEvent;
    return true
               && time.Equals(that.time)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(UnitCounteringEvent that) {
    if (time != that.time) {
      return time.CompareTo(that.time);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "UnitCounteringEvent(" +
        time.DStr()
        + ")";

    }
    public static UnitCounteringEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(")");
      return new UnitCounteringEvent(time);
  }
}
       
}
