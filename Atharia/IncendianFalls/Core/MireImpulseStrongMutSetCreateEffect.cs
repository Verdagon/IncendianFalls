using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MireImpulseStrongMutSetCreateEffect : IMireImpulseStrongMutSetEffect {
  public readonly int id;
  public MireImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IMireImpulseStrongMutSetEffect.id => id;
  public void visitIMireImpulseStrongMutSetEffect(IMireImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitMireImpulseStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMireImpulseStrongMutSetEffect(this);
  }
}

}
