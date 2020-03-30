using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BlastRodCreateEffect : IBlastRodEffect {
  public readonly int id;
  public readonly BlastRodIncarnation incarnation;
  public BlastRodCreateEffect(int id, BlastRodIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBlastRodEffect.id => id;
  public void visitIBlastRodEffect(IBlastRodEffectVisitor visitor) {
    visitor.visitBlastRodCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBlastRodEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
