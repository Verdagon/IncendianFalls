using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloatAnimation {
  float Get(float time);
  IFloatAnimation Simplify(float time);
}
