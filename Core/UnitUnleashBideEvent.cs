using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitUnleashBideEvent : IComparable<UnitUnleashBideEvent> {
  public static readonly string NAME = "UnitUnleashBideEvent";
  public class EqualityComparer : IEqualityComparer<UnitUnleashBideEvent> {
    public bool Equals(UnitUnleashBideEvent a, UnitUnleashBideEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(UnitUnleashBideEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<UnitUnleashBideEvent> {
    public int Compare(UnitUnleashBideEvent a, UnitUnleashBideEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public readonly int attackerId;
  public readonly IntImmList victimIds;
  public UnitUnleashBideEvent(
      int time,
      int attackerId,
      IntImmList victimIds) {
    this.time = time;
    this.attackerId = attackerId;
    this.victimIds = victimIds;
    int hash = 0;
    hash = hash * 37 + time.GetDeterministicHashCode();
    hash = hash * 37 + attackerId.GetDeterministicHashCode();
    hash = hash * 37 + victimIds.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(UnitUnleashBideEvent a, UnitUnleashBideEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(UnitUnleashBideEvent a, UnitUnleashBideEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is UnitUnleashBideEvent)) {
      return false;
    }
    var that = obj as UnitUnleashBideEvent;
    return true
               && time.Equals(that.time)
        && attackerId.Equals(that.attackerId)
        && victimIds.Equals(that.victimIds)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(UnitUnleashBideEvent that) {
    if (time != that.time) {
      return time.CompareTo(that.time);
    }
    if (attackerId != that.attackerId) {
      return attackerId.CompareTo(that.attackerId);
    }
    if (victimIds != that.victimIds) {
      return victimIds.CompareTo(that.victimIds);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "UnitUnleashBideEvent(" +
        time.DStr() + ", " +
        attackerId.DStr() + ", " +
        victimIds.DStr()
        + ")";

    }
    public static UnitUnleashBideEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(",");
      var attackerId = source.ParseInt();
      source.Expect(",");
      var victimIds = IntImmList.Parse(source);
      source.Expect(")");
      return new UnitUnleashBideEvent(time, attackerId, victimIds);
  }
}
       
}
