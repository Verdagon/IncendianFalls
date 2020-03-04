using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class NarrateEvent : IComparable<NarrateEvent> {
  public static readonly string NAME = "NarrateEvent";
  public class EqualityComparer : IEqualityComparer<NarrateEvent> {
    public bool Equals(NarrateEvent a, NarrateEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(NarrateEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<NarrateEvent> {
    public int Compare(NarrateEvent a, NarrateEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly string text;
  public NarrateEvent(
      string text) {
    this.text = text;
    int hash = 0;
    hash = hash * 37 + text.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(NarrateEvent a, NarrateEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(NarrateEvent a, NarrateEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is NarrateEvent)) {
      return false;
    }
    var that = obj as NarrateEvent;
    return true
               && text.Equals(that.text)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(NarrateEvent that) {
    if (text != that.text) {
      return text.CompareTo(that.text);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "NarrateEvent(" +
        text.DStr()
        + ")";

    }
    public static NarrateEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var text = source.ParseStr();
      source.Expect(")");
      return new NarrateEvent(text);
  }
}
       
}
