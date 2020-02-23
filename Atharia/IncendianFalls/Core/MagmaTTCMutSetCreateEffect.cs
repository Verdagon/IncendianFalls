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
  public void visit(IMagmaTTCMutSetEffectVisitor visitor) {
    visitor.visitMagmaTTCMutSetCreateEffect(this);
  }
}

}
