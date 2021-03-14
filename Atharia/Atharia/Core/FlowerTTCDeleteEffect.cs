using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FlowerTTCDeleteEffect : IFlowerTTCEffect {
  public readonly int id;
  public FlowerTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IFlowerTTCEffect.id => id;
  public void visitIFlowerTTCEffect(IFlowerTTCEffectVisitor visitor) {
    visitor.visitFlowerTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFlowerTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
