using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlastRodStrongMutSetDeleteEffect : IBlastRodStrongMutSetEffect {
  public readonly int id;
  public BlastRodStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBlastRodStrongMutSetEffect.id => id;
  public void visitIBlastRodStrongMutSetEffect(IBlastRodStrongMutSetEffectVisitor visitor) {
    visitor.visitBlastRodStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlastRodStrongMutSetEffect(this);
  }
}

}
