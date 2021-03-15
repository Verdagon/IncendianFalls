using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitExplosionEvent : IComparable<UnitExplosionEvent> {
  public static readonly string NAME = "UnitExplosionEvent";
  public class EqualityComparer : IEqualityComparer<UnitExplosionEvent> {
    public bool Equals(UnitExplosionEvent a, UnitExplosionEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(UnitExplosionEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<UnitExplosionEvent> {
    public int Compare(UnitExplosionEvent a, UnitExplosionEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public readonly int attackerId;
  public readonly Location targetLoc;
  public UnitExplosionEvent(
      int time,
      int attackerId,
      Location targetLoc) {
    this.time = time;
    this.attackerId = attackerId;
    this.targetLoc = targetLoc;
    int hash = 0;
    hash = hash * 37 + time.GetDeterministicHashCode();
    hash = hash * 37 + attackerId.GetDeterministicHashCode();
    hash = hash * 37 + targetLoc.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(UnitExplosionEvent a, UnitExplosionEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(UnitExplosionEvent a, UnitExplosionEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is UnitExplosionEvent)) {
      return false;
    }
    var that = obj as UnitExplosionEvent;
    return true
               && time.Equals(that.time)
        && attackerId.Equals(that.attackerId)
        && targetLoc.Equals(that.targetLoc)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(UnitExplosionEvent that) {
    if (time != that.time) {
      return time.CompareTo(that.time);
    }
    if (attackerId != that.attackerId) {
      return attackerId.CompareTo(that.attackerId);
    }
    if (targetLoc != that.targetLoc) {
      return targetLoc.CompareTo(that.targetLoc);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "UnitExplosionEvent(" +
        time.DStr() + ", " +
        attackerId.DStr() + ", " +
        targetLoc.DStr()
        + ")";

    }
    public static UnitExplosionEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(",");
      var attackerId = source.ParseInt();
      source.Expect(",");
      var targetLoc = Location.Parse(source);
      source.Expect(")");
      return new UnitExplosionEvent(time, attackerId, targetLoc);
  }
}
       
}
