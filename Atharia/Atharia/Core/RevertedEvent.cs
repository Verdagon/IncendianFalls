using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RevertedEvent : IComparable<RevertedEvent> {
  public static readonly string NAME = "RevertedEvent";
  public class EqualityComparer : IEqualityComparer<RevertedEvent> {
    public bool Equals(RevertedEvent a, RevertedEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(RevertedEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<RevertedEvent> {
    public int Compare(RevertedEvent a, RevertedEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public RevertedEvent(
) {
    int hash = 0;
    this.hashCode = hash;

  }
  public static bool operator==(RevertedEvent a, RevertedEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(RevertedEvent a, RevertedEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is RevertedEvent)) {
      return false;
    }
    var that = obj as RevertedEvent;
    return true
               ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(RevertedEvent that) {
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "RevertedEvent()";
    }
    public static RevertedEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      source.Expect(")");
      return new RevertedEvent();
  }
}
       
}
