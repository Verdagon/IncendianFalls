using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlastRodMutSetCreateEffect : IBlastRodMutSetEffect {
  public readonly int id;
  public BlastRodMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBlastRodMutSetEffect.id => id;
  public void visit(IBlastRodMutSetEffectVisitor visitor) {
    visitor.visitBlastRodMutSetCreateEffect(this);
  }
}

}
