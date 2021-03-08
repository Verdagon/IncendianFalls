using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MagmaTTCIncarnation : IMagmaTTCEffectVisitor {
  public MagmaTTCIncarnation(
) {
  }
  public MagmaTTCIncarnation Copy() {
    return new MagmaTTCIncarnation(
    );
  }

  public void visitMagmaTTCCreateEffect(MagmaTTCCreateEffect e) {}
  public void visitMagmaTTCDeleteEffect(MagmaTTCDeleteEffect e) {}

  public void ApplyEffect(IMagmaTTCEffect effect) { effect.visitIMagmaTTCEffect(this); }
}

}
