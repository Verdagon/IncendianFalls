using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ITemporaryCloneImpulseEffect : IEffect {
  int id { get; }
  void visitITemporaryCloneImpulseEffect(ITemporaryCloneImpulseEffectVisitor visitor);
}
       
}
