using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIPreActingUCWeakMutBunchEffect : IEffect {
  int id { get; }
  void visitIIPreActingUCWeakMutBunchEffect(IIPreActingUCWeakMutBunchEffectVisitor visitor);
}
       
}
