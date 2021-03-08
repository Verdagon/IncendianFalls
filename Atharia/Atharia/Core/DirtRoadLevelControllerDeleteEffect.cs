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
  public void visitIDirtRoadLevelControllerEffect(IDirtRoadLevelControllerEffectVisitor visitor) {
    visitor.visitDirtRoadLevelControllerDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDirtRoadLevelControllerEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
