using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MireImpulseStrongMutSetAddEffect : IMireImpulseStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MireImpulseStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMireImpulseStrongMutSetEffect.id => id;
  public void visitIMireImpulseStrongMutSetEffect(IMireImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMireImpulseStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMireImpulseStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
