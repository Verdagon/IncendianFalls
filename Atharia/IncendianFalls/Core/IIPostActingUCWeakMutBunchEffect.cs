using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIPostActingUCWeakMutBunchEffect : IEffect {
  int id { get; }
  void visitIIPostActingUCWeakMutBunchEffect(IIPostActingUCWeakMutBunchEffectVisitor visitor);
}
       
}
