using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WaitForAnimationsEvent : IComparable<WaitForAnimationsEvent> {
  public static readonly string NAME = "WaitForAnimationsEvent";
  public class EqualityComparer : IEqualityComparer<WaitForAnimationsEvent> {
    public bool Equals(WaitForAnimationsEvent a, WaitForAnimationsEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(WaitForAnimationsEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<WaitForAnimationsEvent> {
    public int Compare(WaitForAnimationsEvent a, WaitForAnimationsEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public WaitForAnimationsEvent(
) {
    int hash = 0;
    this.hashCode = hash;

  }
  public static bool operator==(WaitForAnimationsEvent a, WaitForAnimationsEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(WaitForAnimationsEvent a, WaitForAnimationsEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is WaitForAnimationsEvent)) {
      return false;
    }
    var that = obj as WaitForAnimationsEvent;
    return true
               ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(WaitForAnimationsEvent that) {
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "WaitForAnimationsEvent()";
    }
    public static WaitForAnimationsEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      source.Expect(")");
      return new WaitForAnimationsEvent();
  }
}
       
}
