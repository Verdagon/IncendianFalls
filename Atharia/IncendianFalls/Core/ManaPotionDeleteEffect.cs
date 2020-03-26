using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ManaPotionDeleteEffect : IManaPotionEffect {
  public readonly int id;
  public ManaPotionDeleteEffect(int id) {
    this.id = id;
  }
  int IManaPotionEffect.id => id;
  public void visitIManaPotionEffect(IManaPotionEffectVisitor visitor) {
    visitor.visitManaPotionDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitManaPotionEffect(this);
  }
}

}
