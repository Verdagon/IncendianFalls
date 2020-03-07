using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BlastRodCreateEffect : IBlastRodEffect {
  public readonly int id;
  public BlastRodCreateEffect(int id) {
    this.id = id;
  }
  int IBlastRodEffect.id => id;
  public void visit(IBlastRodEffectVisitor visitor) {
    visitor.visitBlastRodCreateEffect(this);
  }
}

}
