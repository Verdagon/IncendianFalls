using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IHealthPotionStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIHealthPotionStrongMutSetEffect(IHealthPotionStrongMutSetEffectVisitor visitor);
}

}
