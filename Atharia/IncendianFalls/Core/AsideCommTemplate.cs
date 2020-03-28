using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AsideCommTemplate : IComparable<AsideCommTemplate> {
  public static readonly string NAME = "AsideCommTemplate";
  public class EqualityComparer : IEqualityComparer<AsideCommTemplate> {
    public bool Equals(AsideCommTemplate a, AsideCommTemplate b) {
      return a.Equals(b);
    }
    public int GetHashCode(AsideCommTemplate a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<AsideCommTemplate> {
    public int Compare(AsideCommTemplate a, AsideCommTemplate b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public AsideCommTemplate(
) {
    int hash = 0;
    this.hashCode = hash;

  }
  public static bool operator==(AsideCommTemplate a, AsideCommTemplate b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(AsideCommTemplate a, AsideCommTemplate b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is AsideCommTemplate)) {
      return false;
    }
    var that = obj as AsideCommTemplate;
    return true
               ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(AsideCommTemplate that) {
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "AsideCommTemplate()";
    }
    public static AsideCommTemplate Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      source.Expect(")");
      return new AsideCommTemplate();
  }
}
       
}
