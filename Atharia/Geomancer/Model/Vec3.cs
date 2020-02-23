using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
public class Vec3 : IComparable<Vec3> {
  public static readonly string NAME = "Vec3";
  public class EqualityComparer : IEqualityComparer<Vec3> {
    public bool Equals(Vec3 a, Vec3 b) {
      return a.Equals(b);
    }
    public int GetHashCode(Vec3 a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<Vec3> {
    public int Compare(Vec3 a, Vec3 b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly float x;
  public readonly float y;
  public readonly float z;
  public Vec3(
      float x,
      float y,
      float z) {
    this.x = x;
    this.y = y;
    this.z = z;
    int hash = 0;
    hash = hash * 37 + x.GetDeterministicHashCode();
    hash = hash * 37 + y.GetDeterministicHashCode();
    hash = hash * 37 + z.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(Vec3 a, Vec3 b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(Vec3 a, Vec3 b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is Vec3)) {
      return false;
    }
    var that = obj as Vec3;
    return true
               && x.Equals(that.x)
        && y.Equals(that.y)
        && z.Equals(that.z)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(Vec3 that) {
    if (x != that.x) {
      return x.CompareTo(that.x);
    }
    if (y != that.y) {
      return y.CompareTo(that.y);
    }
    if (z != that.z) {
      return z.CompareTo(that.z);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "Vec3(" +
        x.DStr() + ", " +
        y.DStr() + ", " +
        z.DStr()
        + ")";

    }
    public static Vec3 Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var x = source.ParseFloat();
      source.Expect(",");
      var y = source.ParseFloat();
      source.Expect(",");
      var z = source.ParseFloat();
      source.Expect(")");
      return new Vec3(x, y, z);
  }
}
       
}
