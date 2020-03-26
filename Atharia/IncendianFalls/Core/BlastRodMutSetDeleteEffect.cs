using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BlastRodMutSetDeleteEffect : IBlastRodMutSetEffect {
  public readonly int id;
  public BlastRodMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IBlastRodMutSetEffect.id => id;
  public void visitIBlastRodMutSetEffect(IBlastRodMutSetEffectVisitor visitor) {
    visitor.visitBlastRodMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlastRodMutSetEffect(this);
  }
}

}
