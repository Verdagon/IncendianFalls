using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WaitEvent : IComparable<WaitEvent> {
  public static readonly string NAME = "WaitEvent";
  public class EqualityComparer : IEqualityComparer<WaitEvent> {
    public bool Equals(WaitEvent a, WaitEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(WaitEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<WaitEvent> {
    public int Compare(WaitEvent a, WaitEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int timeMs;
  public readonly string endTriggerName;
  public WaitEvent(
      int timeMs,
      string endTriggerName) {
    this.timeMs = timeMs;
    this.endTriggerName = endTriggerName;
    int hash = 0;
    hash = hash * 37 + timeMs.GetDeterministicHashCode();
    hash = hash * 37 + endTriggerName.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(WaitEvent a, WaitEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(WaitEvent a, WaitEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is WaitEvent)) {
      return false;
    }
    var that = obj as WaitEvent;
    return true
               && timeMs.Equals(that.timeMs)
        && endTriggerName.Equals(that.endTriggerName)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(WaitEvent that) {
    if (timeMs != that.timeMs) {
      return timeMs.CompareTo(that.timeMs);
    }
    if (endTriggerName != that.endTriggerName) {
      return endTriggerName.CompareTo(that.endTriggerName);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "WaitEvent(" +
        timeMs.DStr() + ", " +
        endTriggerName.DStr()
        + ")";

    }
    public static WaitEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var timeMs = source.ParseInt();
      source.Expect(",");
      var endTriggerName = source.ParseStr();
      source.Expect(")");
      return new WaitEvent(timeMs, endTriggerName);
  }
}
       
}
