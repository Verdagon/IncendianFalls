using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IIncendianFallsLevelLinkerTTCEffect : IEffect {
  int id { get; }
  void visitIIncendianFallsLevelLinkerTTCEffect(IIncendianFallsLevelLinkerTTCEffectVisitor visitor);
}
       
}
