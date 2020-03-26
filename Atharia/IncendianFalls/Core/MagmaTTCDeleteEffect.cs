using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MagmaTTCDeleteEffect : IMagmaTTCEffect {
  public readonly int id;
  public MagmaTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IMagmaTTCEffect.id => id;
  public void visitIMagmaTTCEffect(IMagmaTTCEffectVisitor visitor) {
    visitor.visitMagmaTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMagmaTTCEffect(this);
  }
}

}
