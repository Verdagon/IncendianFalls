using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitMireEvent : IComparable<UnitMireEvent> {
  public static readonly string NAME = "UnitMireEvent";
  public class EqualityComparer : IEqualityComparer<UnitMireEvent> {
    public bool Equals(UnitMireEvent a, UnitMireEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(UnitMireEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<UnitMireEvent> {
    public int Compare(UnitMireEvent a, UnitMireEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public readonly int attackerId;
  public readonly int victimId;
  public UnitMireEvent(
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
  public static bool operator==(UnitMireEvent a, UnitMireEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(UnitMireEvent a, UnitMireEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is UnitMireEvent)) {
      return false;
    }
    var that = obj as UnitMireEvent;
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
  public int CompareTo(UnitMireEvent that) {
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
    return "UnitMireEvent(" +
        time.DStr() + ", " +
        attackerId.DStr() + ", " +
        victimId.DStr()
        + ")";

    }
    public static UnitMireEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(",");
      var attackerId = source.ParseInt();
      source.Expect(",");
      var victimId = source.ParseInt();
      source.Expect(")");
      return new UnitMireEvent(time, attackerId, victimId);
  }
}
       
}
