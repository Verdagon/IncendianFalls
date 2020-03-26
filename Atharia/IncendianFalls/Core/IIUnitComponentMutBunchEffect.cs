using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIUnitComponentMutBunchEffect : IEffect {
  int id { get; }
  void visitIIUnitComponentMutBunchEffect(IIUnitComponentMutBunchEffectVisitor visitor);
}
       
}
