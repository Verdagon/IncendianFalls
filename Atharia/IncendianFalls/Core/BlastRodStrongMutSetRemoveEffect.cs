using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlastRodStrongMutSetRemoveEffect : IBlastRodStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BlastRodStrongMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBlastRodStrongMutSetEffect.id => id;
  public void visit(IBlastRodStrongMutSetEffectVisitor visitor) {
    visitor.visitBlastRodStrongMutSetRemoveEffect(this);
  }
}

}
