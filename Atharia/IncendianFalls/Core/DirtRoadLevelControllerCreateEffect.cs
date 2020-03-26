using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct DirtRoadLevelControllerCreateEffect : IDirtRoadLevelControllerEffect {
  public readonly int id;
  public readonly DirtRoadLevelControllerIncarnation incarnation;
  public DirtRoadLevelControllerCreateEffect(int id, DirtRoadLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDirtRoadLevelControllerEffect.id => id;
  public void visitIDirtRoadLevelControllerEffect(IDirtRoadLevelControllerEffectVisitor visitor) {
    visitor.visitDirtRoadLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitDirtRoadLevelControllerEffect(this);
  }
}

}
