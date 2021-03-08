using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MireImpulseStrongMutSetRemoveEffect : IMireImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MireImpulseStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMireImpulseStrongMutSetEffect.id => id;
  public void visitIMireImpulseStrongMutSetEffect(IMireImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMireImpulseStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMireImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
