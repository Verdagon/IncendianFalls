using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUpStaircaseTTCEffect {
  int id { get; }
  void visit(IUpStaircaseTTCEffectVisitor visitor);
}
       
}
