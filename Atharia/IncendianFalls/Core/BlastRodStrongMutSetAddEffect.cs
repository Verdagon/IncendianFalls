using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlastRodStrongMutSetAddEffect : IBlastRodStrongMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BlastRodStrongMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBlastRodStrongMutSetEffect.id => id;
  public void visit(IBlastRodStrongMutSetEffectVisitor visitor) {
    visitor.visitBlastRodStrongMutSetAddEffect(this);
  }
}

}
