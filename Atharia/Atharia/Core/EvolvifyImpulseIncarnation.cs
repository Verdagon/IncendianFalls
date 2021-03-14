using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class EvolvifyImpulseIncarnation : IEvolvifyImpulseEffectVisitor {
  public readonly int weight;
  public readonly Location moveToLocation;
  public EvolvifyImpulseIncarnation(
      int weight,
      Location moveToLocation) {
    this.weight = weight;
    this.moveToLocation = moveToLocation;
  }
  public EvolvifyImpulseIncarnation Copy() {
    return new EvolvifyImpulseIncarnation(
weight,
moveToLocation    );
  }

  public void visitEvolvifyImpulseCreateEffect(EvolvifyImpulseCreateEffect e) {}
  public void visitEvolvifyImpulseDeleteEffect(EvolvifyImpulseDeleteEffect e) {}


  public void ApplyEffect(IEvolvifyImpulseEffect effect) { effect.visitIEvolvifyImpulseEffect(this); }
}

}
