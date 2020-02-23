using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BideAICapabilityUCSetChargeEffect : IBideAICapabilityUCEffect {
  public readonly int id;
  public readonly int newValue;
  public BideAICapabilityUCSetChargeEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IBideAICapabilityUCEffect.id => id;

  public void visit(IBideAICapabilityUCEffectVisitor visitor) {
    visitor.visitBideAICapabilityUCSetChargeEffect(this);
  }
}

}
