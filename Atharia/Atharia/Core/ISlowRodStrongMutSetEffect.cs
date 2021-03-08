using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISlowRodStrongMutSetEffect : IEffect {
  int id { get; }
  void visitISlowRodStrongMutSetEffect(ISlowRodStrongMutSetEffectVisitor visitor);
}

}
