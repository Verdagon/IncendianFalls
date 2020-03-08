using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISlowRodEffect {
  int id { get; }
  void visit(ISlowRodEffectVisitor visitor);
}
       
}
