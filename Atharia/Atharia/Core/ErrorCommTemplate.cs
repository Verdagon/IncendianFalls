using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ErrorCommTemplate : IComparable<ErrorCommTemplate> {
  public static readonly string NAME = "ErrorCommTemplate";
  public class EqualityComparer : IEqualityComparer<ErrorCommTemplate> {
    public bool Equals(ErrorCommTemplate a, ErrorCommTemplate b) {
      return a.Equals(b);
    }
    public int GetHashCode(ErrorCommTemplate a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<ErrorCommTemplate> {
    public int Compare(ErrorCommTemplate a, ErrorCommTemplate b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public ErrorCommTemplate(
) {
    int hash = 0;
    this.hashCode = hash;

  }
  public static bool operator==(ErrorCommTemplate a, ErrorCommTemplate b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(ErrorCommTemplate a, ErrorCommTemplate b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is ErrorCommTemplate)) {
      return false;
    }
    var that = obj as ErrorCommTemplate;
    return true
               ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(ErrorCommTemplate that) {
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "ErrorCommTemplate()";
    }
    public static ErrorCommTemplate Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      source.Expect(")");
      return new ErrorCommTemplate();
  }
}
       
}
