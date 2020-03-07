using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BlastRodDeleteEffect : IBlastRodEffect {
  public readonly int id;
  public BlastRodDeleteEffect(int id) {
    this.id = id;
  }
  int IBlastRodEffect.id => id;
  public void visit(IBlastRodEffectVisitor visitor) {
    visitor.visitBlastRodDeleteEffect(this);
  }
}

}