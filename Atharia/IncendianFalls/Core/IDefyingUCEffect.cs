using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDefyingUCEffect {
  int id { get; }
  void visit(IDefyingUCEffectVisitor visitor);
}
       
}
