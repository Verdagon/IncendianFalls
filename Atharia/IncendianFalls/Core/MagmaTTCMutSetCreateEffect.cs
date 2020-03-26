using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MagmaTTCMutSetCreateEffect : IMagmaTTCMutSetEffect {
  public readonly int id;
  public MagmaTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IMagmaTTCMutSetEffect.id => id;
  public void visitIMagmaTTCMutSetEffect(IMagmaTTCMutSetEffectVisitor visitor) {
    visitor.visitMagmaTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMagmaTTCMutSetEffect(this);
  }
}

}
