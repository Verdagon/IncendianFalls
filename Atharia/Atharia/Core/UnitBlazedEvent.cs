using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitBlazedEvent : IComparable<UnitBlazedEvent> {
  public static readonly string NAME = "UnitBlazedEvent";
  public class EqualityComparer : IEqualityComparer<UnitBlazedEvent> {
    public bool Equals(UnitBlazedEvent a, UnitBlazedEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(UnitBlazedEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<UnitBlazedEvent> {
    public int Compare(UnitBlazedEvent a, UnitBlazedEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public readonly int victimId;
  public readonly Location location;
  public UnitBlazedEvent(
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
  public static bool operator==(UnitBlazedEvent a, UnitBlazedEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(UnitBlazedEvent a, UnitBlazedEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is UnitBlazedEvent)) {
      return false;
    }
    var that = obj as UnitBlazedEvent;
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
  public int CompareTo(UnitBlazedEvent that) {
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
    return "UnitBlazedEvent(" +
        time.DStr() + ", " +
        victimId.DStr() + ", " +
        location.DStr()
        + ")";

    }
    public static UnitBlazedEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(",");
      var victimId = source.ParseInt();
      source.Expect(",");
      var location = Location.Parse(source);
      source.Expect(")");
      return new UnitBlazedEvent(time, victimId, location);
  }
}
       
}
