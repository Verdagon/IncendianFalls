using System;

namespace Geomancer.Model {
  public static class Vec3Extensions {
    public static float distance(this Vec3 a, Vec3 that) {
      return (float)Math.Sqrt(
          (that.x - a.x) * (that.x - a.x) +
          (that.y - a.y) * (that.y - a.y) +
          (that.z - a.z) * (that.z - a.z));
    }

    public static Vec3 mul(this Vec3 v, float f) {
      return new Vec3(v.x * f, v.y * f, v.z * f);
    }

    public static Vec3 plus(this Vec3 a, Vec3 b) {
      return new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static float dot(this Vec3 a, Vec3 that) {
      return a.x * that.x + a.y * that.y + a.z * that.z;
    }
  }
}
