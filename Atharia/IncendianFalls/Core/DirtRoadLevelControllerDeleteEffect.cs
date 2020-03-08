using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DirtRoadLevelControllerDeleteEffect : IDirtRoadLevelControllerEffect {
  public readonly int id;
  public DirtRoadLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int IDirtRoadLevelControllerEffect.id => id;
  public void visit(IDirtRoadLevelControllerEffectVisitor visitor) {
    visitor.visitDirtRoadLevelControllerDeleteEffect(this);
  }
}

}
