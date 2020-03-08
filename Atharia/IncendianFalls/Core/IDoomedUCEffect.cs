using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDoomedUCEffect {
  int id { get; }
  void visit(IDoomedUCEffectVisitor visitor);
}
       
}
