using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantFloatAnimation : IFloatAnimation {
  float value;

  public ConstantFloatAnimation(float value) {
    this.value = value;
  }

  public float Get(float time) {
    return value;
  }

  public IFloatAnimation Simplify(float time) {
    return this;
  }
}
