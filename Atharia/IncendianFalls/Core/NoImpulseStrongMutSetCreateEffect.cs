using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct NoImpulseStrongMutSetCreateEffect : INoImpulseStrongMutSetEffect {
  public readonly int id;
  public NoImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int INoImpulseStrongMutSetEffect.id => id;
  public void visit(INoImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitNoImpulseStrongMutSetCreateEffect(this);
  }
}

}
