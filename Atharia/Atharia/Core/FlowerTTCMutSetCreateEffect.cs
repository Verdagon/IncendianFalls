using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FlowerTTCMutSetCreateEffect : IFlowerTTCMutSetEffect {
  public readonly int id;
  public FlowerTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IFlowerTTCMutSetEffect.id => id;
  public void visitIFlowerTTCMutSetEffect(IFlowerTTCMutSetEffectVisitor visitor) {
    visitor.visitFlowerTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFlowerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
