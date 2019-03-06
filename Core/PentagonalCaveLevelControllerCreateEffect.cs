using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct PentagonalCaveLevelControllerCreateEffect : IPentagonalCaveLevelControllerEffect {
  public readonly int id;
  public PentagonalCaveLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int IPentagonalCaveLevelControllerEffect.id => id;
  public void visit(IPentagonalCaveLevelControllerEffectVisitor visitor) {
    visitor.visitPentagonalCaveLevelControllerCreateEffect(this);
  }
}

}
