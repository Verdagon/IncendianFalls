using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MagmaTTCCreateEffect : IMagmaTTCEffect {
  public readonly int id;
  public MagmaTTCCreateEffect(int id) {
    this.id = id;
  }
  int IMagmaTTCEffect.id => id;
  public void visit(IMagmaTTCEffectVisitor visitor) {
    visitor.visitMagmaTTCCreateEffect(this);
  }
}

}
