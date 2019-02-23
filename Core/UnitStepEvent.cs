using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitStepEvent : IComparable<UnitStepEvent> {
  public static readonly string NAME = "UnitStepEvent";
  public class EqualityComparer : IEqualityComparer<UnitStepEvent> {
    public bool Equals(UnitStepEvent a, UnitStepEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(UnitStepEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<UnitStepEvent> {
    public int Compare(UnitStepEvent a, UnitStepEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public readonly int unitId;
  public readonly Location from;
  public readonly Location to;
  public UnitStepEvent(
      int time,
      int unitId,
      Location from,
      Location to) {
    this.time = time;
    this.unitId = unitId;
    this.from = from;
    this.to = to;
    int hash = 0;
    hash = hash * 37 + time.GetDeterministicHashCode();
    hash = hash * 37 + unitId.GetDeterministicHashCode();
    hash = hash * 37 + from.GetDeterministicHashCode();
    hash = hash * 37 + to.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(UnitStepEvent a, UnitStepEvent b) {
    return a.Equals(b);
  }
  public static bool operator!=(UnitStepEvent a, UnitStepEvent b) {
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is UnitStepEvent)) {
      return false;
    }
    var that = obj as UnitStepEvent;
    return true
               && time.Equals(that.time)
        && unitId.Equals(that.unitId)
        && from.Equals(that.from)
        && to.Equals(that.to)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(UnitStepEvent that) {
    if (time != that.time) {
      return time.CompareTo(that.time);
    }
    if (unitId != that.unitId) {
      return unitId.CompareTo(that.unitId);
    }
    if (from != that.from) {
      return from.CompareTo(that.from);
    }
    if (to != that.to) {
      return to.CompareTo(that.to);
    }
    return 0;
  }
  public string DStr() {
    return "UnitStepEvent(" +
        time.DStr() + ", " +
        unitId.DStr() + ", " +
        from.DStr() + ", " +
        to.DStr()
        + ")";

    }
    public static UnitStepEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(",");
      var unitId = source.ParseInt();
      source.Expect(",");
      var from = Location.Parse(source);
      source.Expect(",");
      var to = Location.Parse(source);
      source.Expect(")");
      return new UnitStepEvent(time, unitId, from, to);
  }
}
       
}
