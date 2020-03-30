using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class WaitForCameraEvent : IComparable<WaitForCameraEvent> {
  public static readonly string NAME = "WaitForCameraEvent";
  public class EqualityComparer : IEqualityComparer<WaitForCameraEvent> {
    public bool Equals(WaitForCameraEvent a, WaitForCameraEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(WaitForCameraEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<WaitForCameraEvent> {
    public int Compare(WaitForCameraEvent a, WaitForCameraEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public WaitForCameraEvent(
) {
    int hash = 0;
    this.hashCode = hash;

  }
  public static bool operator==(WaitForCameraEvent a, WaitForCameraEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(WaitForCameraEvent a, WaitForCameraEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is WaitForCameraEvent)) {
      return false;
    }
    var that = obj as WaitForCameraEvent;
    return true
               ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(WaitForCameraEvent that) {
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "WaitForCameraEvent()";
    }
    public static WaitForCameraEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      source.Expect(")");
      return new WaitForCameraEvent();
  }
}
       
}
