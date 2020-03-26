using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGlaiveStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIGlaiveStrongMutSetEffect(IGlaiveStrongMutSetEffectVisitor visitor);
}

}
