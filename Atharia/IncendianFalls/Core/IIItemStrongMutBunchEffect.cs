using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIItemStrongMutBunchEffect : IEffect {
  int id { get; }
  void visitIIItemStrongMutBunchEffect(IIItemStrongMutBunchEffectVisitor visitor);
}
       
}
