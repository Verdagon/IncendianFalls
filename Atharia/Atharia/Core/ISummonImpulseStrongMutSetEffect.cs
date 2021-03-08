using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISummonImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitISummonImpulseStrongMutSetEffect(ISummonImpulseStrongMutSetEffectVisitor visitor);
}

}
