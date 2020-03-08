using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IObsidianFloorTTCEffect {
  int id { get; }
  void visit(IObsidianFloorTTCEffectVisitor visitor);
}
       
}
