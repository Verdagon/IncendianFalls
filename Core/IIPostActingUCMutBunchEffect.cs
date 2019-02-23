using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIPostActingUCMutBunchEffect {
  int id { get; }
  void visit(IIPostActingUCMutBunchEffectVisitor visitor);
}
       
}
