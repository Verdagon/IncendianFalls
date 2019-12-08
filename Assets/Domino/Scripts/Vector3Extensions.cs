using System;

using UnityEngine;

public static class Vector3Extensions {
  public static bool EqualsE(this Vector3 a, Vector3 b, float epsilon) {
    for (int i = 0; i < 3; i++) {
      if (Math.Abs(a[i] - b[i]) > epsilon) {
        return false;
      }
    }
    return true;
  }
}
