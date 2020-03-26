using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireTTCMutSetAddEffect : IFireTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FireTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFireTTCMutSetEffect.id => id;
  public void visitIFireTTCMutSetEffect(IFireTTCMutSetEffectVisitor visitor) {
    visitor.visitFireTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireTTCMutSetEffect(this);
  }
}

}
