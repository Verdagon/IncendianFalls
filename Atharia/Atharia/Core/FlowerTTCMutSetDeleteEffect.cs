using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FlowerTTCMutSetDeleteEffect : IFlowerTTCMutSetEffect {
  public readonly int id;
  public FlowerTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IFlowerTTCMutSetEffect.id => id;
  public void visitIFlowerTTCMutSetEffect(IFlowerTTCMutSetEffectVisitor visitor) {
    visitor.visitFlowerTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFlowerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
