using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MoveImpulseIncarnation : IMoveImpulseEffectVisitor {
  public readonly int weight;
  public readonly Location stepLocation;
  public MoveImpulseIncarnation(
      int weight,
      Location stepLocation) {
    this.weight = weight;
    this.stepLocation = stepLocation;
  }
  public MoveImpulseIncarnation Copy() {
    return new MoveImpulseIncarnation(
weight,
stepLocation    );
  }

  public void visitMoveImpulseCreateEffect(MoveImpulseCreateEffect e) {}
  public void visitMoveImpulseDeleteEffect(MoveImpulseDeleteEffect e) {}


  public void ApplyEffect(IMoveImpulseEffect effect) { effect.visitIMoveImpulseEffect(this); }
}

}
