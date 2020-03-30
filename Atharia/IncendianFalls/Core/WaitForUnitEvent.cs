using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WaitForUnitEvent : IComparable<WaitForUnitEvent> {
  public static readonly string NAME = "WaitForUnitEvent";
  public class EqualityComparer : IEqualityComparer<WaitForUnitEvent> {
    public bool Equals(WaitForUnitEvent a, WaitForUnitEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(WaitForUnitEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<WaitForUnitEvent> {
    public int Compare(WaitForUnitEvent a, WaitForUnitEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int unitId;
  public WaitForUnitEvent(
      int unitId) {
    this.unitId = unitId;
    int hash = 0;
    hash = hash * 37 + unitId.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(WaitForUnitEvent a, WaitForUnitEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(WaitForUnitEvent a, WaitForUnitEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is WaitForUnitEvent)) {
      return false;
    }
    var that = obj as WaitForUnitEvent;
    return true
               && unitId.Equals(that.unitId)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(WaitForUnitEvent that) {
    if (unitId != that.unitId) {
      return unitId.CompareTo(that.unitId);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "WaitForUnitEvent(" +
        unitId.DStr()
        + ")";

    }
    public static WaitForUnitEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var unitId = source.ParseInt();
      source.Expect(")");
      return new WaitForUnitEvent(unitId);
  }
}
       
}
