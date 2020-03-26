using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIImpulseStrongMutBunchEffect : IEffect {
  int id { get; }
  void visitIIImpulseStrongMutBunchEffect(IIImpulseStrongMutBunchEffectVisitor visitor);
}
       
}
