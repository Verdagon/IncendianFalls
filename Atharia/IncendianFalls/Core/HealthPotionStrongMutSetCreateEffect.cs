using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionStrongMutSetCreateEffect : IHealthPotionStrongMutSetEffect {
  public readonly int id;
  public HealthPotionStrongMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IHealthPotionStrongMutSetEffect.id => id;
  public void visitIHealthPotionStrongMutSetEffect(IHealthPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionStrongMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHealthPotionStrongMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
