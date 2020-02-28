using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIncendianFallsLevelLinkerTTCMutSetEffect {
  int id { get; }
  void visit(IIncendianFallsLevelLinkerTTCMutSetEffectVisitor visitor);
}

}
