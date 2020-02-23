using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMatrix4x4Animation {
  Matrix4x4 Get(long timeMs);
  IMatrix4x4Animation Simplify(long timeMs);
}
