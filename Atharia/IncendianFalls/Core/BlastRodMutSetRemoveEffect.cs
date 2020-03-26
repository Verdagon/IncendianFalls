using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlastRodMutSetRemoveEffect : IBlastRodMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BlastRodMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBlastRodMutSetEffect.id => id;
  public void visitIBlastRodMutSetEffect(IBlastRodMutSetEffectVisitor visitor) {
    visitor.visitBlastRodMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlastRodMutSetEffect(this);
  }
}

}
