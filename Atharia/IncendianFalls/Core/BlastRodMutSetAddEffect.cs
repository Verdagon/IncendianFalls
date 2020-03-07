using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlastRodMutSetAddEffect : IBlastRodMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BlastRodMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBlastRodMutSetEffect.id => id;
  public void visit(IBlastRodMutSetEffectVisitor visitor) {
    visitor.visitBlastRodMutSetAddEffect(this);
  }
}

}
