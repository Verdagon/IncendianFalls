using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IEvaporateImpulseEffect {
  int id { get; }
  void visit(IEvaporateImpulseEffectVisitor visitor);
}
       
}
