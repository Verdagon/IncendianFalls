using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct HealthPotionCreateEffect : IHealthPotionEffect {
  public readonly int id;
  public HealthPotionCreateEffect(int id) {
    this.id = id;
  }
  int IHealthPotionEffect.id => id;
  public void visit(IHealthPotionEffectVisitor visitor) {
    visitor.visitHealthPotionCreateEffect(this);
  }
}

}
