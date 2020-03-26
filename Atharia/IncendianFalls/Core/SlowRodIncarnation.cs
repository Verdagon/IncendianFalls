using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SlowRodIncarnation : ISlowRodEffectVisitor {
  public SlowRodIncarnation(
) {
  }
  public SlowRodIncarnation Copy() {
    return new SlowRodIncarnation(
    );
  }

  public void visitSlowRodCreateEffect(SlowRodCreateEffect e) {}
  public void visitSlowRodDeleteEffect(SlowRodDeleteEffect e) {}

  public void ApplyEffect(ISlowRodEffect effect) { effect.visitISlowRodEffect(this); }
}

}
