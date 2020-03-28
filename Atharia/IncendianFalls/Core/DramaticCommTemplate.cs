using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DramaticCommTemplate : IComparable<DramaticCommTemplate> {
  public static readonly string NAME = "DramaticCommTemplate";
  public class EqualityComparer : IEqualityComparer<DramaticCommTemplate> {
    public bool Equals(DramaticCommTemplate a, DramaticCommTemplate b) {
      return a.Equals(b);
    }
    public int GetHashCode(DramaticCommTemplate a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<DramaticCommTemplate> {
    public int Compare(DramaticCommTemplate a, DramaticCommTemplate b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly bool isObscuring;
  public DramaticCommTemplate(
      bool isObscuring) {
    this.isObscuring = isObscuring;
    int hash = 0;
    hash = hash * 37 + isObscuring.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(DramaticCommTemplate a, DramaticCommTemplate b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(DramaticCommTemplate a, DramaticCommTemplate b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is DramaticCommTemplate)) {
      return false;
    }
    var that = obj as DramaticCommTemplate;
    return true
               && isObscuring.Equals(that.isObscuring)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(DramaticCommTemplate that) {
    if (isObscuring != that.isObscuring) {
      return isObscuring.CompareTo(that.isObscuring);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "DramaticCommTemplate(" +
        isObscuring.DStr()
        + ")";

    }
    public static DramaticCommTemplate Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var isObscuring = source.ParseBool();
      source.Expect(")");
      return new DramaticCommTemplate(isObscuring);
  }
}
       
}
