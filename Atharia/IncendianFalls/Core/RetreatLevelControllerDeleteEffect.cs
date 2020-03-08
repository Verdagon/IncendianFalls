using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RetreatLevelControllerDeleteEffect : IRetreatLevelControllerEffect {
  public readonly int id;
  public RetreatLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int IRetreatLevelControllerEffect.id => id;
  public void visit(IRetreatLevelControllerEffectVisitor visitor) {
    visitor.visitRetreatLevelControllerDeleteEffect(this);
  }
}

}
