using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIncendianFallsLevelLinkerTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIIncendianFallsLevelLinkerTTCMutSetEffect(IIncendianFallsLevelLinkerTTCMutSetEffectVisitor visitor);
}

}
