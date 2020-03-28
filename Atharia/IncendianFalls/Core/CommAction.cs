using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CommAction : IComparable<CommAction> {
  public static readonly string NAME = "CommAction";
  public class EqualityComparer : IEqualityComparer<CommAction> {
    public bool Equals(CommAction a, CommAction b) {
      return a.Equals(b);
    }
    public int GetHashCode(CommAction a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<CommAction> {
    public int Compare(CommAction a, CommAction b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly string label;
  public readonly string triggerName;
  public CommAction(
      string label,
      string triggerName) {
    this.label = label;
    this.triggerName = triggerName;
    int hash = 0;
    hash = hash * 37 + label.GetDeterministicHashCode();
    hash = hash * 37 + triggerName.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(CommAction a, CommAction b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(CommAction a, CommAction b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is CommAction)) {
      return false;
    }
    var that = obj as CommAction;
    return true
               && label.Equals(that.label)
        && triggerName.Equals(that.triggerName)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(CommAction that) {
    if (label != that.label) {
      return label.CompareTo(that.label);
    }
    if (triggerName != that.triggerName) {
      return triggerName.CompareTo(that.triggerName);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "CommAction(" +
        label.DStr() + ", " +
        triggerName.DStr()
        + ")";

    }
    public static CommAction Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var label = source.ParseStr();
      source.Expect(",");
      var triggerName = source.ParseStr();
      source.Expect(")");
      return new CommAction(label, triggerName);
  }
}
       
}
