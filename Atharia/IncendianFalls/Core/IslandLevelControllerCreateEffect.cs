using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IslandLevelControllerCreateEffect : IIslandLevelControllerEffect {
  public readonly int id;
  public IslandLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int IIslandLevelControllerEffect.id => id;
  public void visit(IIslandLevelControllerEffectVisitor visitor) {
    visitor.visitIslandLevelControllerCreateEffect(this);
  }
}

}
