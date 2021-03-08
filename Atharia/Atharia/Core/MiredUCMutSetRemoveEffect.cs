using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MiredUCMutSetRemoveEffect : IMiredUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MiredUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMiredUCMutSetEffect.id => id;
  public void visitIMiredUCMutSetEffect(IMiredUCMutSetEffectVisitor visitor) {
    visitor.visitMiredUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMiredUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
