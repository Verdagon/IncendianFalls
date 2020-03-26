using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MiredUCWeakMutSetRemoveEffect : IMiredUCWeakMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MiredUCWeakMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMiredUCWeakMutSetEffect.id => id;
  public void visitIMiredUCWeakMutSetEffect(IMiredUCWeakMutSetEffectVisitor visitor) {
    visitor.visitMiredUCWeakMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMiredUCWeakMutSetEffect(this);
  }
}

}
