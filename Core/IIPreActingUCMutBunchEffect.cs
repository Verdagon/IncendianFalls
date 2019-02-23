using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIPreActingUCMutBunchEffect {
  int id { get; }
  void visit(IIPreActingUCMutBunchEffectVisitor visitor);
}
       
}
