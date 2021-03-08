using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class NormalCommTemplate : IComparable<NormalCommTemplate> {
  public static readonly string NAME = "NormalCommTemplate";
  public class EqualityComparer : IEqualityComparer<NormalCommTemplate> {
    public bool Equals(NormalCommTemplate a, NormalCommTemplate b) {
      return a.Equals(b);
    }
    public int GetHashCode(NormalCommTemplate a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<NormalCommTemplate> {
    public int Compare(NormalCommTemplate a, NormalCommTemplate b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public NormalCommTemplate(
) {
    int hash = 0;
    this.hashCode = hash;

  }
  public static bool operator==(NormalCommTemplate a, NormalCommTemplate b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(NormalCommTemplate a, NormalCommTemplate b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is NormalCommTemplate)) {
      return false;
    }
    var that = obj as NormalCommTemplate;
    return true
               ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(NormalCommTemplate that) {
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "NormalCommTemplate()";
    }
    public static NormalCommTemplate Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      source.Expect(")");
      return new NormalCommTemplate();
  }
}
       
}
