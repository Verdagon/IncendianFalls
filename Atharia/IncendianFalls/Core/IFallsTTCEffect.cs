using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFallsTTCEffect {
  int id { get; }
  void visit(IFallsTTCEffectVisitor visitor);
}
       
}
