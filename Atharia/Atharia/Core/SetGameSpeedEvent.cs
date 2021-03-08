using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SetGameSpeedEvent : IComparable<SetGameSpeedEvent> {
  public static readonly string NAME = "SetGameSpeedEvent";
  public class EqualityComparer : IEqualityComparer<SetGameSpeedEvent> {
    public bool Equals(SetGameSpeedEvent a, SetGameSpeedEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(SetGameSpeedEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<SetGameSpeedEvent> {
    public int Compare(SetGameSpeedEvent a, SetGameSpeedEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int percent;
  public SetGameSpeedEvent(
      int percent) {
    this.percent = percent;
    int hash = 0;
    hash = hash * 37 + percent.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(SetGameSpeedEvent a, SetGameSpeedEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(SetGameSpeedEvent a, SetGameSpeedEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is SetGameSpeedEvent)) {
      return false;
    }
    var that = obj as SetGameSpeedEvent;
    return true
               && percent.Equals(that.percent)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(SetGameSpeedEvent that) {
    if (percent != that.percent) {
      return percent.CompareTo(that.percent);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "SetGameSpeedEvent(" +
        percent.DStr()
        + ")";

    }
    public static SetGameSpeedEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var percent = source.ParseInt();
      source.Expect(")");
      return new SetGameSpeedEvent(percent);
  }
}
       
}
