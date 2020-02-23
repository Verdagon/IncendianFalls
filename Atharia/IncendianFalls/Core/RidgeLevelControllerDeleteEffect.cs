using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RidgeLevelControllerDeleteEffect : IRidgeLevelControllerEffect {
  public readonly int id;
  public RidgeLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int IRidgeLevelControllerEffect.id => id;
  public void visit(IRidgeLevelControllerEffectVisitor visitor) {
    visitor.visitRidgeLevelControllerDeleteEffect(this);
  }
}

}
