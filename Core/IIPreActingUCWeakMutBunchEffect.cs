using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIPreActingUCWeakMutBunchEffect {
  int id { get; }
  void visit(IIPreActingUCWeakMutBunchEffectVisitor visitor);
}
       
}
