using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IManaPotionEffectVisitor {
  void visitManaPotionCreateEffect(ManaPotionCreateEffect effect);
  void visitManaPotionDeleteEffect(ManaPotionDeleteEffect effect);
}

}
