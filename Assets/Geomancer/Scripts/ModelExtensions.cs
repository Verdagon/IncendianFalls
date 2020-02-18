using System;
using UnityEngine;
using Geomancer.Model;

namespace Geomancer {
  public static class ModelExtensions {
    public static Vector3 ToUnity(this Vec3 vec3) {
      return new Vector3(vec3.x, vec3.z, vec3.y);
    }
  }
}
