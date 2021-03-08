using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IFloorTTCEffect : IEffect {
  int id { get; }
  void visitIFloorTTCEffect(IFloorTTCEffectVisitor visitor);
}
       
}
