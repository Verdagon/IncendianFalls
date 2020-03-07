using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IWarperTTCEffect {
  int id { get; }
  void visit(IWarperTTCEffectVisitor visitor);
}
       
}
