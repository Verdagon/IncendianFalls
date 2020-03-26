using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireBombImpulseIncarnation : IFireBombImpulseEffectVisitor {
  public readonly int weight;
  public readonly Location location;
  public FireBombImpulseIncarnation(
      int weight,
      Location location) {
    this.weight = weight;
    this.location = location;
  }
  public FireBombImpulseIncarnation Copy() {
    return new FireBombImpulseIncarnation(
weight,
location    );
  }

  public void visitFireBombImpulseCreateEffect(FireBombImpulseCreateEffect e) {}
  public void visitFireBombImpulseDeleteEffect(FireBombImpulseDeleteEffect e) {}


  public void ApplyEffect(IFireBombImpulseEffect effect) { effect.visitIFireBombImpulseEffect(this); }
}

}
