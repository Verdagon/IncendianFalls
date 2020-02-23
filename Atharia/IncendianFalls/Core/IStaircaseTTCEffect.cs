using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IStaircaseTTCEffect {
  int id { get; }
  void visit(IStaircaseTTCEffectVisitor visitor);
}
       
}
