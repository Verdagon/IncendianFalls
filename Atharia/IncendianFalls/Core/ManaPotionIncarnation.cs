using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ManaPotionIncarnation : IManaPotionEffectVisitor {
  public ManaPotionIncarnation(
) {
  }
  public ManaPotionIncarnation Copy() {
    return new ManaPotionIncarnation(
    );
  }

  public void visitManaPotionCreateEffect(ManaPotionCreateEffect e) {}
  public void visitManaPotionDeleteEffect(ManaPotionDeleteEffect e) {}

  public void ApplyEffect(IManaPotionEffect effect) { effect.visitIManaPotionEffect(this); }
}

}
