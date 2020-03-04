using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFloorTTCEffect {
  int id { get; }
  void visit(IFloorTTCEffectVisitor visitor);
}
       
}
