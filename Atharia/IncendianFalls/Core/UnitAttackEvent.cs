using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitAttackEvent : IComparable<UnitAttackEvent> {
  public static readonly string NAME = "UnitAttackEvent";
  public class EqualityComparer : IEqualityComparer<UnitAttackEvent> {
    public bool Equals(UnitAttackEvent a, UnitAttackEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(UnitAttackEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<UnitAttackEvent> {
    public int Compare(UnitAttackEvent a, UnitAttackEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public readonly int attackerId;
  public readonly int victimId;
  public UnitAttackEvent(
      int time,
      int attackerId,
      int victimId) {
    this.time = time;
    this.attackerId = attackerId;
    this.victimId = victimId;
    int hash = 0;
    hash = hash * 37 + time.GetDeterministicHashCode();
    hash = hash * 37 + attackerId.GetDeterministicHashCode();
    hash = hash * 37 + victimId.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(UnitAttackEvent a, UnitAttackEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(UnitAttackEvent a, UnitAttackEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is UnitAttackEvent)) {
      return false;
    }
    var that = obj as UnitAttackEvent;
    return true
               && time.Equals(that.time)
        && attackerId.Equals(that.attackerId)
        && victimId.Equals(that.victimId)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(UnitAttackEvent that) {
    if (time != that.time) {
      return time.CompareTo(that.time);
    }
    if (attackerId != that.attackerId) {
      return attackerId.CompareTo(that.attackerId);
    }
    if (victimId != that.victimId) {
      return victimId.CompareTo(that.victimId);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "UnitAttackEvent(" +
        time.DStr() + ", " +
        attackerId.DStr() + ", " +
        victimId.DStr()
        + ")";

    }
    public static UnitAttackEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(",");
      var attackerId = source.ParseInt();
      source.Expect(",");
      var victimId = source.ParseInt();
      source.Expect(")");
      return new UnitAttackEvent(time, attackerId, victimId);
  }
}
       
}
