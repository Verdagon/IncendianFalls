using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RidgeLevelControllerCreateEffect : IRidgeLevelControllerEffect {
  public readonly int id;
  public RidgeLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int IRidgeLevelControllerEffect.id => id;
  public void visit(IRidgeLevelControllerEffectVisitor visitor) {
    visitor.visitRidgeLevelControllerCreateEffect(this);
  }
}

}
