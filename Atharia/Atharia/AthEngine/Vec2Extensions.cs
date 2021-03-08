using System;

namespace Atharia.Model {
  public static class Vec2Extensions {
    public static float distance(this Vec2 a, Vec2 b) {
      return (float)Math.Sqrt(
          (b.x - a.x) * (b.x - a.x) +
          (b.y - a.y) * (b.y - a.y));
    }

    public static Vec2 mul(this Vec2 v, float f) {
      return new Vec2(v.x * f, v.y * f);
    }

    public static Vec2 div(this Vec2 v, float f) {
      return new Vec2(v.x / f, v.y / f);
    }

    public static Vec2 plus(this Vec2 a, Vec2 b) {
      return new Vec2(a.x + b.x, a.y + b.y);
    }

    public static Vec2 minus(this Vec2 a, Vec2 b) {
      return new Vec2(a.x - b.x, a.y - b.y);
    }

    public static float dot(this Vec2 a, Vec2 that) {
      return a.x * that.x + a.y * that.y;
    }

    public static Vec2 minimums(this Vec2 a, Vec2 that) {
      return new Vec2(Math.Min(a.x, that.x), Math.Min(a.y, that.y));
    }

    public static Vec2 maximums(this Vec2 a, Vec2 that) {
      return new Vec2(Math.Max(a.x, that.x), Math.Max(a.y, that.y));
    }
  }
}
