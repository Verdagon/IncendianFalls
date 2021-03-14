using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FlowerTTCMutSetAddEffect : IFlowerTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FlowerTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFlowerTTCMutSetEffect.id => id;
  public void visitIFlowerTTCMutSetEffect(IFlowerTTCMutSetEffectVisitor visitor) {
    visitor.visitFlowerTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFlowerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
