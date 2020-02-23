using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MagmaTTCMutSetRemoveEffect : IMagmaTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MagmaTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMagmaTTCMutSetEffect.id => id;
  public void visit(IMagmaTTCMutSetEffectVisitor visitor) {
    visitor.visitMagmaTTCMutSetRemoveEffect(this);
  }
}

}
