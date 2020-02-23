using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIPostActingUCWeakMutBunchEffect {
  int id { get; }
  void visit(IIPostActingUCWeakMutBunchEffectVisitor visitor);
}
       
}
