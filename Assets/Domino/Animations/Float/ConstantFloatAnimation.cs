using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFloatAnimation : IFloatAnimation {
  float value;

  public ConstantFloatAnimation(float value) {
    this.value = value;
  }

  public float Get(long timeMs) {
    return value;
  }

  public IFloatAnimation Simplify(long timeMs) {
    return this;
  }
}
