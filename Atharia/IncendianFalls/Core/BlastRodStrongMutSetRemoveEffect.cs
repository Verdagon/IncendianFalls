using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlastRodStrongMutSetRemoveEffect : IBlastRodStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BlastRodStrongMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBlastRodStrongMutSetEffect.id => id;
  public void visitIBlastRodStrongMutSetEffect(IBlastRodStrongMutSetEffectVisitor visitor) {
    visitor.visitBlastRodStrongMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlastRodStrongMutSetEffect(this);
  }
}

}
