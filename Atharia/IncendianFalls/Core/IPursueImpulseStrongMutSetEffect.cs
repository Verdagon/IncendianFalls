using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IPursueImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIPursueImpulseStrongMutSetEffect(IPursueImpulseStrongMutSetEffectVisitor visitor);
}

}
