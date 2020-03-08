using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DirtRoadLevelControllerCreateEffect : IDirtRoadLevelControllerEffect {
  public readonly int id;
  public DirtRoadLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int IDirtRoadLevelControllerEffect.id => id;
  public void visit(IDirtRoadLevelControllerEffectVisitor visitor) {
    visitor.visitDirtRoadLevelControllerCreateEffect(this);
  }
}

}
