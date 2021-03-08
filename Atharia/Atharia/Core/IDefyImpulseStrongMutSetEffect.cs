using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefyImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIDefyImpulseStrongMutSetEffect(IDefyImpulseStrongMutSetEffectVisitor visitor);
}

}
