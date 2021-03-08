using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class Color : IComparable<Color> {
  public static readonly string NAME = "Color";
  public class EqualityComparer : IEqualityComparer<Color> {
    public bool Equals(Color a, Color b) {
      return a.Equals(b);
    }
    public int GetHashCode(Color a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<Color> {
    public int Compare(Color a, Color b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int r;
  public readonly int g;
  public readonly int b;
  public readonly int a;
  public Color(
      int r,
      int g,
      int b,
      int a) {
    this.r = r;
    this.g = g;
    this.b = b;
    this.a = a;
    int hash = 0;
    hash = hash * 37 + r.GetDeterministicHashCode();
    hash = hash * 37 + g.GetDeterministicHashCode();
    hash = hash * 37 + b.GetDeterministicHashCode();
    hash = hash * 37 + a.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(Color a, Color b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(Color a, Color b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is Color)) {
      return false;
    }
    var that = obj as Color;
    return true
               && r.Equals(that.r)
        && g.Equals(that.g)
        && b.Equals(that.b)
        && a.Equals(that.a)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(Color that) {
    if (r != that.r) {
      return r.CompareTo(that.r);
    }
    if (g != that.g) {
      return g.CompareTo(that.g);
    }
    if (b != that.b) {
      return b.CompareTo(that.b);
    }
    if (a != that.a) {
      return a.CompareTo(that.a);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "Color(" +
        r.DStr() + ", " +
        g.DStr() + ", " +
        b.DStr() + ", " +
        a.DStr()
        + ")";

    }
    public static Color Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var r = source.ParseInt();
      source.Expect(",");
      var g = source.ParseInt();
      source.Expect(",");
      var b = source.ParseInt();
      source.Expect(",");
      var a = source.ParseInt();
      source.Expect(")");
      return new Color(r, g, b, a);
  }
}
       
}
