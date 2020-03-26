using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ManaPotionCreateEffect : IManaPotionEffect {
  public readonly int id;
  public readonly ManaPotionIncarnation incarnation;
  public ManaPotionCreateEffect(int id, ManaPotionIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IManaPotionEffect.id => id;
  public void visitIManaPotionEffect(IManaPotionEffectVisitor visitor) {
    visitor.visitManaPotionCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitManaPotionEffect(this);
  }
}

}
