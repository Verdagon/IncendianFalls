using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MagmaTTCMutSetDeleteEffect : IMagmaTTCMutSetEffect {
  public readonly int id;
  public MagmaTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IMagmaTTCMutSetEffect.id => id;
  public void visitIMagmaTTCMutSetEffect(IMagmaTTCMutSetEffectVisitor visitor) {
    visitor.visitMagmaTTCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMagmaTTCMutSetEffect(this);
  }
}

}
