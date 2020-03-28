using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CommText : IComparable<CommText> {
  public static readonly string NAME = "CommText";
  public class EqualityComparer : IEqualityComparer<CommText> {
    public bool Equals(CommText a, CommText b) {
      return a.Equals(b);
    }
    public int GetHashCode(CommText a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<CommText> {
    public int Compare(CommText a, CommText b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly string speakerRole;
  public readonly string text;
  public CommText(
      string speakerRole,
      string text) {
    this.speakerRole = speakerRole;
    this.text = text;
    int hash = 0;
    hash = hash * 37 + speakerRole.GetDeterministicHashCode();
    hash = hash * 37 + text.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(CommText a, CommText b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(CommText a, CommText b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is CommText)) {
      return false;
    }
    var that = obj as CommText;
    return true
               && speakerRole.Equals(that.speakerRole)
        && text.Equals(that.text)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(CommText that) {
    if (speakerRole != that.speakerRole) {
      return speakerRole.CompareTo(that.speakerRole);
    }
    if (text != that.text) {
      return text.CompareTo(that.text);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "CommText(" +
        speakerRole.DStr() + ", " +
        text.DStr()
        + ")";

    }
    public static CommText Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var speakerRole = source.ParseStr();
      source.Expect(",");
      var text = source.ParseStr();
      source.Expect(")");
      return new CommText(speakerRole, text);
  }
}
       
}
