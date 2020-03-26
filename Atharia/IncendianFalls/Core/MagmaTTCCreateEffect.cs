using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MagmaTTCCreateEffect : IMagmaTTCEffect {
  public readonly int id;
  public readonly MagmaTTCIncarnation incarnation;
  public MagmaTTCCreateEffect(int id, MagmaTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IMagmaTTCEffect.id => id;
  public void visitIMagmaTTCEffect(IMagmaTTCEffectVisitor visitor) {
    visitor.visitMagmaTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMagmaTTCEffect(this);
  }
}

}
