using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class HealthPotionIncarnation : IHealthPotionEffectVisitor {
  public HealthPotionIncarnation(
) {
  }
  public HealthPotionIncarnation Copy() {
    return new HealthPotionIncarnation(
    );
  }

  public void visitHealthPotionCreateEffect(HealthPotionCreateEffect e) {}
  public void visitHealthPotionDeleteEffect(HealthPotionDeleteEffect e) {}

  public void ApplyEffect(IHealthPotionEffect effect) { effect.visitIHealthPotionEffect(this); }
}

}
