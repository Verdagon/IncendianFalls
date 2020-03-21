using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitFireBombedEvent : IComparable<UnitFireBombedEvent> {
  public static readonly string NAME = "UnitFireBombedEvent";
  public class EqualityComparer : IEqualityComparer<UnitFireBombedEvent> {
    public bool Equals(UnitFireBombedEvent a, UnitFireBombedEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(UnitFireBombedEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<UnitFireBombedEvent> {
    public int Compare(UnitFireBombedEvent a, UnitFireBombedEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public readonly int victimId;
  public readonly Location location;
  public UnitFireBombedEvent(
      int time,
      int victimId,
      Location location) {
    this.time = time;
    this.victimId = victimId;
    this.location = location;
    int hash = 0;
    hash = hash * 37 + time.GetDeterministicHashCode();
    hash = hash * 37 + victimId.GetDeterministicHashCode();
    hash = hash * 37 + location.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(UnitFireBombedEvent a, UnitFireBombedEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(UnitFireBombedEvent a, UnitFireBombedEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is UnitFireBombedEvent)) {
      return false;
    }
    var that = obj as UnitFireBombedEvent;
    return true
               && time.Equals(that.time)
        && victimId.Equals(that.victimId)
        && location.Equals(that.location)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(UnitFireBombedEvent that) {
    if (time != that.time) {
      return time.CompareTo(that.time);
    }
    if (victimId != that.victimId) {
      return victimId.CompareTo(that.victimId);
    }
    if (location != that.location) {
      return location.CompareTo(that.location);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "UnitFireBombedEvent(" +
        time.DStr() + ", " +
        victimId.DStr() + ", " +
        location.DStr()
        + ")";

    }
    public static UnitFireBombedEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(",");
      var victimId = source.ParseInt();
      source.Expect(",");
      var location = Location.Parse(source);
      source.Expect(")");
      return new UnitFireBombedEvent(time, victimId, location);
  }
}
       
}
