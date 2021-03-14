using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FlowerTTCMutSetRemoveEffect : IFlowerTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FlowerTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFlowerTTCMutSetEffect.id => id;
  public void visitIFlowerTTCMutSetEffect(IFlowerTTCMutSetEffectVisitor visitor) {
    visitor.visitFlowerTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFlowerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
