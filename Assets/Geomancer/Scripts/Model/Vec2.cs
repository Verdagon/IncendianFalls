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
         public readonly int x;
  public readonly int y;
  public Vec2(
      int x,
      int y) {
    this.x = x;
    this.y = y;
    int hash = 0;
    hash = hash * 37 + x;
    hash = hash * 37 + y;
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
        x + ", " +
        y
        + ")";

    }
    public static Vec2 Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var x = source.ParseInt();
      source.Expect(",");
      var y = source.ParseInt();
      source.Expect(")");
      return new Vec2(x, y);
  }
    
    
    public int distance(Vec2 b) {
      return (int)Math.Sqrt(
          (b.x - this.x) * (b.x - this.x) +
          (b.y - this.y) * (b.y - this.y));
    }

    public Vec2 mul(int f) {
      return new Vec2(this.x * f, this.y * f);
    }

    public Vec2 div(int f) {
      return new Vec2(this.x / f, this.y / f);
    }

    public Vec2 plus(Vec2 b) {
      return new Vec2(this.x + b.x, this.y + b.y);
    }

    public Vec2 minus(Vec2 b) {
      return new Vec2(this.x - b.x, this.y - b.y);
    }

    public int dot(Vec2 that) {
      return this.x * that.x + this.y * that.y;
    }

    public Vec2 minimums(Vec2 that) {
      return new Vec2(Math.Min(this.x, that.x), Math.Min(this.y, that.y));
    }

    public Vec2 maximums(Vec2 that) {
      return new Vec2(Math.Max(this.x, that.x), Math.Max(this.y, that.y));
    }
}
       
}
