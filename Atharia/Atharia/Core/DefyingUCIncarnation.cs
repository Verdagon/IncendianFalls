using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefyingUCIncarnation : IDefyingUCEffectVisitor {
  public DefyingUCIncarnation(
) {
  }
  public DefyingUCIncarnation Copy() {
    return new DefyingUCIncarnation(
    );
  }

  public void visitDefyingUCCreateEffect(DefyingUCCreateEffect e) {}
  public void visitDefyingUCDeleteEffect(DefyingUCDeleteEffect e) {}

  public void ApplyEffect(IDefyingUCEffect effect) { effect.visitIDefyingUCEffect(this); }
}

}
