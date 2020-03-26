using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ManaPotionStrongMutSetDeleteEffect : IManaPotionStrongMutSetEffect {
  public readonly int id;
  public ManaPotionStrongMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IManaPotionStrongMutSetEffect.id => id;
  public void visitIManaPotionStrongMutSetEffect(IManaPotionStrongMutSetEffectVisitor visitor) {
    visitor.visitManaPotionStrongMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitManaPotionStrongMutSetEffect(this);
  }
}

}
