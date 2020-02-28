using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IItemTTCEffect {
  int id { get; }
  void visit(IItemTTCEffectVisitor visitor);
}
       
}
