using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FlowerTTCIncarnation : IFlowerTTCEffectVisitor {
  public FlowerTTCIncarnation(
) {
  }
  public FlowerTTCIncarnation Copy() {
    return new FlowerTTCIncarnation(
    );
  }

  public void visitFlowerTTCCreateEffect(FlowerTTCCreateEffect e) {}
  public void visitFlowerTTCDeleteEffect(FlowerTTCDeleteEffect e) {}

  public void ApplyEffect(IFlowerTTCEffect effect) { effect.visitIFlowerTTCEffect(this); }
}

}
