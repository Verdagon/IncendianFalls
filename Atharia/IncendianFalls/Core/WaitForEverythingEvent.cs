using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WaitForEverythingEvent : IComparable<WaitForEverythingEvent> {
  public static readonly string NAME = "WaitForEverythingEvent";
  public class EqualityComparer : IEqualityComparer<WaitForEverythingEvent> {
    public bool Equals(WaitForEverythingEvent a, WaitForEverythingEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(WaitForEverythingEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<WaitForEverythingEvent> {
    public int Compare(WaitForEverythingEvent a, WaitForEverythingEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public WaitForEverythingEvent(
) {
    int hash = 0;
    this.hashCode = hash;

  }
  public static bool operator==(WaitForEverythingEvent a, WaitForEverythingEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(WaitForEverythingEvent a, WaitForEverythingEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is WaitForEverythingEvent)) {
      return false;
    }
    var that = obj as WaitForEverythingEvent;
    return true
               ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(WaitForEverythingEvent that) {
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "WaitForEverythingEvent()";
    }
    public static WaitForEverythingEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      source.Expect(")");
      return new WaitForEverythingEvent();
  }
}
       
}
