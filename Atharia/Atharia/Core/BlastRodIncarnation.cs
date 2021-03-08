using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlastRodIncarnation : IBlastRodEffectVisitor {
  public BlastRodIncarnation(
) {
  }
  public BlastRodIncarnation Copy() {
    return new BlastRodIncarnation(
    );
  }

  public void visitBlastRodCreateEffect(BlastRodCreateEffect e) {}
  public void visitBlastRodDeleteEffect(BlastRodDeleteEffect e) {}

  public void ApplyEffect(IBlastRodEffect effect) { effect.visitIBlastRodEffect(this); }
}

}
