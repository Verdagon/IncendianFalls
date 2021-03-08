using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IManaPotionStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIManaPotionStrongMutSetEffect(IManaPotionStrongMutSetEffectVisitor visitor);
}

}
