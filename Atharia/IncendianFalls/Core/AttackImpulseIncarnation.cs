using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AttackImpulseIncarnation : IAttackImpulseEffectVisitor {
  public readonly int weight;
  public readonly int targetUnit;
  public AttackImpulseIncarnation(
      int weight,
      int targetUnit) {
    this.weight = weight;
    this.targetUnit = targetUnit;
  }
  public AttackImpulseIncarnation Copy() {
    return new AttackImpulseIncarnation(
weight,
targetUnit    );
  }

  public void visitAttackImpulseCreateEffect(AttackImpulseCreateEffect e) {}
  public void visitAttackImpulseDeleteEffect(AttackImpulseDeleteEffect e) {}


  public void ApplyEffect(IAttackImpulseEffect effect) { effect.visitIAttackImpulseEffect(this); }
}

}
