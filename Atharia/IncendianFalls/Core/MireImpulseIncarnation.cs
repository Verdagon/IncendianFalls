using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MireImpulseIncarnation : IMireImpulseEffectVisitor {
  public readonly int weight;
  public readonly int targetUnit;
  public MireImpulseIncarnation(
      int weight,
      int targetUnit) {
    this.weight = weight;
    this.targetUnit = targetUnit;
  }
  public MireImpulseIncarnation Copy() {
    return new MireImpulseIncarnation(
weight,
targetUnit    );
  }

  public void visitMireImpulseCreateEffect(MireImpulseCreateEffect e) {}
  public void visitMireImpulseDeleteEffect(MireImpulseDeleteEffect e) {}


  public void ApplyEffect(IMireImpulseEffect effect) { effect.visitIMireImpulseEffect(this); }
}

}
