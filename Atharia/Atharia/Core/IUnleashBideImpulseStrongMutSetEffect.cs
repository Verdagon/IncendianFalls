using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUnleashBideImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIUnleashBideImpulseStrongMutSetEffect(IUnleashBideImpulseStrongMutSetEffectVisitor visitor);
}

}
