using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CliffLevelControllerDeleteEffect : ICliffLevelControllerEffect {
  public readonly int id;
  public CliffLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int ICliffLevelControllerEffect.id => id;
  public void visit(ICliffLevelControllerEffectVisitor visitor) {
    visitor.visitCliffLevelControllerDeleteEffect(this);
  }
}

}
