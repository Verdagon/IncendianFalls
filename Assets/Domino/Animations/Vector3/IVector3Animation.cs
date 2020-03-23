using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVector3Animation {
  Vector3 Get(long timeMs);
  IVector3Animation Simplify(long timeMs);
}
