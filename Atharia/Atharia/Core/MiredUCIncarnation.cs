using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MiredUCIncarnation : IMiredUCEffectVisitor {
  public MiredUCIncarnation(
) {
  }
  public MiredUCIncarnation Copy() {
    return new MiredUCIncarnation(
    );
  }

  public void visitMiredUCCreateEffect(MiredUCCreateEffect e) {}
  public void visitMiredUCDeleteEffect(MiredUCDeleteEffect e) {}

  public void ApplyEffect(IMiredUCEffect effect) { effect.visitIMiredUCEffect(this); }
}

}
