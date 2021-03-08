using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class NoImpulseIncarnation : INoImpulseEffectVisitor {
  public NoImpulseIncarnation(
) {
  }
  public NoImpulseIncarnation Copy() {
    return new NoImpulseIncarnation(
    );
  }

  public void visitNoImpulseCreateEffect(NoImpulseCreateEffect e) {}
  public void visitNoImpulseDeleteEffect(NoImpulseDeleteEffect e) {}

  public void ApplyEffect(INoImpulseEffect effect) { effect.visitINoImpulseEffect(this); }
}

}
