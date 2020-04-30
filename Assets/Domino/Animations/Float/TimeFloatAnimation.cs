using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFloatAnimation : IFloatAnimation {
  public float Get(long timeMs) {
    return timeMs;
  }

  public IFloatAnimation Simplify(long timeMs) {
    return this;
  }
}
