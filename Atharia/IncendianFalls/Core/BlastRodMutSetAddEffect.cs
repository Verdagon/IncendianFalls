using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlastRodMutSetAddEffect : IBlastRodMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BlastRodMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBlastRodMutSetEffect.id => id;
  public void visitIBlastRodMutSetEffect(IBlastRodMutSetEffectVisitor visitor) {
    visitor.visitBlastRodMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlastRodMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
