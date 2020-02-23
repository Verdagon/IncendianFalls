using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
public class Vec2 : IComparable<Vec2> {
  public static readonly string NAME = "Vec2";
  public class EqualityComparer : IEqualityComparer<Vec2> {
    public bool Equals(Vec2 a, Vec2 b) {
      return a.Equals(b);
    }
    public int GetHashCode(Vec2 a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<Vec2> {
    public int Compare(Vec2 a, Vec2 b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly float x;
  public readonly float y;
  public Vec2(
      float x,
      float y) {
    this.x = x;
    this.y = y;
    int hash = 0;
    hash = hash * 37 + x.GetDeterministicHashCode();
    hash = hash * 37 + y.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(Vec2 a, Vec2 b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(Vec2 a, Vec2 b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is Vec2)) {
      return false;
    }
    var that = obj as Vec2;
    return true
               && x.Equals(that.x)
        && y.Equals(that.y)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(Vec2 that) {
    if (x != that.x) {
      return x.CompareTo(that.x);
    }
    if (y != that.y) {
      return y.CompareTo(that.y);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "Vec2(" +
        x.DStr() + ", " +
        y.DStr()
        + ")";

    }
    public static Vec2 Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var x = source.ParseFloat();
      source.Expect(",");
      var y = source.ParseFloat();
      source.Expect(")");
      return new Vec2(x, y);
  }
}
       
}
