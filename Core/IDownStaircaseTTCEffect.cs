using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDownStaircaseTTCEffect {
  int id { get; }
  void visit(IDownStaircaseTTCEffectVisitor visitor);
}
       
}