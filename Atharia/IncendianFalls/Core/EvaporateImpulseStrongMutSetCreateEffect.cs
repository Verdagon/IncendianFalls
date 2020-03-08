using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct EvaporateImpulseStrongMutSetCreateEffect : IEvaporateImpulseStrongMutSetEffect {
  public readonly int id;
  public EvaporateImpulseStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IEvaporateImpulseStrongMutSetEffect.id => id;
  public void visit(IEvaporateImpulseStrongMutSetEffectVisitor visitor) {
    visitor.visitEvaporateImpulseStrongMutSetCreateEffect(this);
  }
}

}
