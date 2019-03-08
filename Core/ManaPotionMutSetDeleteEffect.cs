using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ManaPotionMutSetDeleteEffect : IManaPotionMutSetEffect {
  public readonly int id;
  public ManaPotionMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IManaPotionMutSetEffect.id => id;
  public void visit(IManaPotionMutSetEffectVisitor visitor) {
    visitor.visitManaPotionMutSetDeleteEffect(this);
  }
}

}
