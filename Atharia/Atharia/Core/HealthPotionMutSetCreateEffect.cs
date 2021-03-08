using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct HealthPotionMutSetCreateEffect : IHealthPotionMutSetEffect {
  public readonly int id;
  public HealthPotionMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IHealthPotionMutSetEffect.id => id;
  public void visitIHealthPotionMutSetEffect(IHealthPotionMutSetEffectVisitor visitor) {
    visitor.visitHealthPotionMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitHealthPotionMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
