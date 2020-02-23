using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IHealthPotionEffectVisitor {
  void visitHealthPotionCreateEffect(HealthPotionCreateEffect effect);
  void visitHealthPotionDeleteEffect(HealthPotionDeleteEffect effect);
}

}
