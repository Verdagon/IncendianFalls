using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BloodTTCMutSetAddEffect : IBloodTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BloodTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBloodTTCMutSetEffect.id => id;
  public void visitIBloodTTCMutSetEffect(IBloodTTCMutSetEffectVisitor visitor) {
    visitor.visitBloodTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBloodTTCMutSetEffect(this);
  }
}

}
