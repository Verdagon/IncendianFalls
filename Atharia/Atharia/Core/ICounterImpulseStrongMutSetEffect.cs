using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICounterImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitICounterImpulseStrongMutSetEffect(ICounterImpulseStrongMutSetEffectVisitor visitor);
}

}
