using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineFloatAnimation : IFloatAnimation {
  IFloatAnimation inner;

  public SineFloatAnimation(IFloatAnimation inner) {
    this.inner = inner;
  }

  public float Get(long timeMs) {
    return Mathf.Sin(inner.Get(timeMs));
  }

  public IFloatAnimation Simplify(long timeMs) {
    return this;
  }
}
