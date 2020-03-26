using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMoveImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIMoveImpulseStrongMutSetEffect(IMoveImpulseStrongMutSetEffectVisitor visitor);
}

}
