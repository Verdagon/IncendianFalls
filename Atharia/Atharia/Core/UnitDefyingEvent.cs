using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitDefyingEvent : IComparable<UnitDefyingEvent> {
  public static readonly string NAME = "UnitDefyingEvent";
  public class EqualityComparer : IEqualityComparer<UnitDefyingEvent> {
    public bool Equals(UnitDefyingEvent a, UnitDefyingEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(UnitDefyingEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<UnitDefyingEvent> {
    public int Compare(UnitDefyingEvent a, UnitDefyingEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int time;
  public UnitDefyingEvent(
      int time) {
    this.time = time;
    int hash = 0;
    hash = hash * 37 + time.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(UnitDefyingEvent a, UnitDefyingEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(UnitDefyingEvent a, UnitDefyingEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is UnitDefyingEvent)) {
      return false;
    }
    var that = obj as UnitDefyingEvent;
    return true
               && time.Equals(that.time)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(UnitDefyingEvent that) {
    if (time != that.time) {
      return time.CompareTo(that.time);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "UnitDefyingEvent(" +
        time.DStr()
        + ")";

    }
    public static UnitDefyingEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var time = source.ParseInt();
      source.Expect(")");
      return new UnitDefyingEvent(time);
  }
}
       
}
