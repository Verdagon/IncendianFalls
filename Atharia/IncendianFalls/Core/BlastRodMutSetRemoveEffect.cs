using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlastRodMutSetRemoveEffect : IBlastRodMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BlastRodMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBlastRodMutSetEffect.id => id;
  public void visit(IBlastRodMutSetEffectVisitor visitor) {
    visitor.visitBlastRodMutSetRemoveEffect(this);
  }
}

}
