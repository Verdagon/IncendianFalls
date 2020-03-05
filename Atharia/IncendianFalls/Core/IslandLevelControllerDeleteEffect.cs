using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IslandLevelControllerDeleteEffect : IIslandLevelControllerEffect {
  public readonly int id;
  public IslandLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int IIslandLevelControllerEffect.id => id;
  public void visit(IIslandLevelControllerEffectVisitor visitor) {
    visitor.visitIslandLevelControllerDeleteEffect(this);
  }
}

}
