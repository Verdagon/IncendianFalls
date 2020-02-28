using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIItemStrongMutBunchEffect {
  int id { get; }
  void visit(IIItemStrongMutBunchEffectVisitor visitor);
}
       
}
