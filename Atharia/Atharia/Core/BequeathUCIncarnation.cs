using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BequeathUCIncarnation : IBequeathUCEffectVisitor {
  public readonly string blueprintName;
  public BequeathUCIncarnation(
      string blueprintName) {
    this.blueprintName = blueprintName;
  }
  public BequeathUCIncarnation Copy() {
    return new BequeathUCIncarnation(
blueprintName    );
  }

  public void visitBequeathUCCreateEffect(BequeathUCCreateEffect e) {}
  public void visitBequeathUCDeleteEffect(BequeathUCDeleteEffect e) {}

  public void ApplyEffect(IBequeathUCEffect effect) { effect.visitIBequeathUCEffect(this); }
}

}
