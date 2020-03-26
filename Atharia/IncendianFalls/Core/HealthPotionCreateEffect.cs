using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct HealthPotionCreateEffect : IHealthPotionEffect {
  public readonly int id;
  public readonly HealthPotionIncarnation incarnation;
  public HealthPotionCreateEffect(int id, HealthPotionIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IHealthPotionEffect.id => id;
  public void visitIHealthPotionEffect(IHealthPotionEffectVisitor visitor) {
    visitor.visitHealthPotionCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHealthPotionEffect(this);
  }
}

}
