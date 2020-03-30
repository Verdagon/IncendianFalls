using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlastRodStrongMutSetAddEffect : IBlastRodStrongMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BlastRodStrongMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBlastRodStrongMutSetEffect.id => id;
  public void visitIBlastRodStrongMutSetEffect(IBlastRodStrongMutSetEffectVisitor visitor) {
    visitor.visitBlastRodStrongMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlastRodStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
