using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIFireImpulseStrongMutSetEffect(IFireImpulseStrongMutSetEffectVisitor visitor);
}

}
