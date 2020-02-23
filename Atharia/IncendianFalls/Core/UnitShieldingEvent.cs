using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitShieldingEvent : IComparable<UnitShieldingEvent> {
  public static readonly string NAME = "UnitShieldingEvent";
  public class EqualityComparer : IEqualityComparer<UnitShieldingEvent> {
    public bool Equals(UnitShieldingEvent a, UnitShieldingEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(UnitShieldingEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<UnitShieldingEvent> {
    public int Compare(UnitShieldingEvent a, UnitShieldingEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public UnitShieldingEvent(
      int time) {
    this.time = time;
    int hash = 0;
    hash = hash * 37 + time.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(UnitShieldingEvent a, UnitShieldingEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(UnitShieldingEvent a, UnitShieldingEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is UnitShieldingEvent)) {
      return false;
    }
    var that = obj as UnitShieldingEvent;
    return true
               && time.Equals(that.time)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(UnitShieldingEvent that) {
    if (time != that.time) {
      return time.CompareTo(that.time);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "UnitShieldingEvent(" +
        time.DStr()
        + ")";

    }
    public static UnitShieldingEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(")");
      return new UnitShieldingEvent(time);
  }
}
       
}
