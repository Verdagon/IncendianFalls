using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFireImpulseEffect {
  int id { get; }
  void visit(IFireImpulseEffectVisitor visitor);
}
       
}
