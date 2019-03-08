using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ManaPotionCreateEffect : IManaPotionEffect {
  public readonly int id;
  public ManaPotionCreateEffect(int id) {
    this.id = id;
  }
  int IManaPotionEffect.id => id;
  public void visit(IManaPotionEffectVisitor visitor) {
    visitor.visitManaPotionCreateEffect(this);
  }
}

}
