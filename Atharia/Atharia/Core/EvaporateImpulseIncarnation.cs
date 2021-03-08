using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class EvaporateImpulseIncarnation : IEvaporateImpulseEffectVisitor {
  public EvaporateImpulseIncarnation(
) {
  }
  public EvaporateImpulseIncarnation Copy() {
    return new EvaporateImpulseIncarnation(
    );
  }

  public void visitEvaporateImpulseCreateEffect(EvaporateImpulseCreateEffect e) {}
  public void visitEvaporateImpulseDeleteEffect(EvaporateImpulseDeleteEffect e) {}

  public void ApplyEffect(IEvaporateImpulseEffect effect) { effect.visitIEvaporateImpulseEffect(this); }
}

}
