using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MagmaTTCMutSetRemoveEffect : IMagmaTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MagmaTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMagmaTTCMutSetEffect.id => id;
  public void visitIMagmaTTCMutSetEffect(IMagmaTTCMutSetEffectVisitor visitor) {
    visitor.visitMagmaTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMagmaTTCMutSetEffect(this);
  }
}

}
