using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlastRodStrongMutSetCreateEffect : IBlastRodStrongMutSetEffect {
  public readonly int id;
  public BlastRodStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBlastRodStrongMutSetEffect.id => id;
  public void visitIBlastRodStrongMutSetEffect(IBlastRodStrongMutSetEffectVisitor visitor) {
    visitor.visitBlastRodStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlastRodStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
