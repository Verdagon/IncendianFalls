using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class UnitStepEvent : IComparable<UnitStepEvent> {
  public static readonly string NAME = "UnitStepEvent";
  public class EqualityComparer : IEqualityComparer<UnitStepEvent> {
    public bool Equals(UnitStepEvent a, UnitStepEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(UnitStepEvent a) {
      return a.GetHashCode();
    }
  }
  public class Comparer : IComparer<UnitStepEvent> {
    public int Compare(UnitStepEvent a, UnitStepEvent b) {
      return a.CompareTo(b);
    }
  }
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
    int hash = 0;
    hash = hash * 1337;
    hash = hash + time.GetHashCode();
    hash = hash * 1337;
    hash = hash + unitId.GetHashCode();
    hash = hash * 1337;
    hash = hash + from.GetHashCode();
    hash = hash * 1337;
    hash = hash + to.GetHashCode();
    return hash;
  }
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
        time + ", " +
        unitId + ", " +
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
