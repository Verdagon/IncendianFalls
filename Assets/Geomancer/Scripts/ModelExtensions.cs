using System;
using UnityEngine;
using Geomancer.Model;

namespace Geomancer {
  public static class ModelExtensions {
    public const float ModelToUnityMultiplier = .001f;
    
    public static Vector3 ToUnity(this Vec3 vec3) {
      return new Vector3(vec3.x * ModelToUnityMultiplier, vec3.z * ModelToUnityMultiplier, vec3.y * ModelToUnityMultiplier);
    }
  }
}
