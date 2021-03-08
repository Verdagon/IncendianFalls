using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DirtRoadLevelControllerIncarnation : IDirtRoadLevelControllerEffectVisitor {
  public readonly int level;
  public DirtRoadLevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public DirtRoadLevelControllerIncarnation Copy() {
    return new DirtRoadLevelControllerIncarnation(
level    );
  }

  public void visitDirtRoadLevelControllerCreateEffect(DirtRoadLevelControllerCreateEffect e) {}
  public void visitDirtRoadLevelControllerDeleteEffect(DirtRoadLevelControllerDeleteEffect e) {}

  public void ApplyEffect(IDirtRoadLevelControllerEffect effect) { effect.visitIDirtRoadLevelControllerEffect(this); }
}

}
