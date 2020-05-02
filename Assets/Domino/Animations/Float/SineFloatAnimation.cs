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

  public static IFloatAnimation Make(float multiplyTimeInput = 1f, float min = 0, float max = 1) {
    return
      new AddFloatAnimation(
        new MultiplyFloatAnimation(
          new AddFloatAnimation(
          new MultiplyFloatAnimation(
            new SineFloatAnimation(
              new MultiplyFloatAnimation(
                new TimeFloatAnimation(),
                new ConstantFloatAnimation(Mathf.PI / 1000 * multiplyTimeInput))),
            new ConstantFloatAnimation(.5f)),
          new ConstantFloatAnimation(.5f)),
        new ConstantFloatAnimation(max - min)),
      new ConstantFloatAnimation(min));
  }
}
