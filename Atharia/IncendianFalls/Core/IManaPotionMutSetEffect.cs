using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IManaPotionMutSetEffect : IEffect {
  int id { get; }
  void visitIManaPotionMutSetEffect(IManaPotionMutSetEffectVisitor visitor);
}

}
