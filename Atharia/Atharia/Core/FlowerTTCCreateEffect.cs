using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FlowerTTCCreateEffect : IFlowerTTCEffect {
  public readonly int id;
  public readonly FlowerTTCIncarnation incarnation;
  public FlowerTTCCreateEffect(int id, FlowerTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IFlowerTTCEffect.id => id;
  public void visitIFlowerTTCEffect(IFlowerTTCEffectVisitor visitor) {
    visitor.visitFlowerTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFlowerTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
