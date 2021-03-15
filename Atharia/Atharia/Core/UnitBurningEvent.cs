using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitBurningEvent : IComparable<UnitBurningEvent> {
  public static readonly string NAME = "UnitBurningEvent";
  public class EqualityComparer : IEqualityComparer<UnitBurningEvent> {
    public bool Equals(UnitBurningEvent a, UnitBurningEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(UnitBurningEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<UnitBurningEvent> {
    public int Compare(UnitBurningEvent a, UnitBurningEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public readonly int victimId;
  public UnitBurningEvent(
      int time,
      int victimId) {
    this.time = time;
    this.victimId = victimId;
    int hash = 0;
    hash = hash * 37 + time.GetDeterministicHashCode();
    hash = hash * 37 + victimId.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(UnitBurningEvent a, UnitBurningEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(UnitBurningEvent a, UnitBurningEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is UnitBurningEvent)) {
      return false;
    }
    var that = obj as UnitBurningEvent;
    return true
               && time.Equals(that.time)
        && victimId.Equals(that.victimId)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(UnitBurningEvent that) {
    if (time != that.time) {
      return time.CompareTo(that.time);
    }
    if (victimId != that.victimId) {
      return victimId.CompareTo(that.victimId);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "UnitBurningEvent(" +
        time.DStr() + ", " +
        victimId.DStr()
        + ")";

    }
    public static UnitBurningEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(",");
      var victimId = source.ParseInt();
      source.Expect(")");
      return new UnitBurningEvent(time, victimId);
  }
}
       
}
