using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class InstructionsCommTemplate : IComparable<InstructionsCommTemplate> {
  public static readonly string NAME = "InstructionsCommTemplate";
  public class EqualityComparer : IEqualityComparer<InstructionsCommTemplate> {
    public bool Equals(InstructionsCommTemplate a, InstructionsCommTemplate b) {
      return a.Equals(b);
    }
    public int GetHashCode(InstructionsCommTemplate a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<InstructionsCommTemplate> {
    public int Compare(InstructionsCommTemplate a, InstructionsCommTemplate b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public InstructionsCommTemplate(
) {
    int hash = 0;
    this.hashCode = hash;

  }
  public static bool operator==(InstructionsCommTemplate a, InstructionsCommTemplate b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(InstructionsCommTemplate a, InstructionsCommTemplate b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is InstructionsCommTemplate)) {
      return false;
    }
    var that = obj as InstructionsCommTemplate;
    return true
               ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(InstructionsCommTemplate that) {
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "InstructionsCommTemplate()";
    }
    public static InstructionsCommTemplate Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      source.Expect(")");
      return new InstructionsCommTemplate();
  }
}
       
}
