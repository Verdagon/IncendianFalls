using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IHealthPotionMutSetEffect : IEffect {
  int id { get; }
  void visitIHealthPotionMutSetEffect(IHealthPotionMutSetEffectVisitor visitor);
}

}
