using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISpeedRingEffect {
  int id { get; }
  void visit(ISpeedRingEffectVisitor visitor);
}
       
}
