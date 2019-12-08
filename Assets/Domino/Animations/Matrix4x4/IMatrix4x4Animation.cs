using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMatrix4x4Animation {
  Matrix4x4 Get(float time);
  IMatrix4x4Animation Simplify(float time);
}
