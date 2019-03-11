using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GauntletLevelControllerCreateEffect : IGauntletLevelControllerEffect {
  public readonly int id;
  public GauntletLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int IGauntletLevelControllerEffect.id => id;
  public void visit(IGauntletLevelControllerEffectVisitor visitor) {
    visitor.visitGauntletLevelControllerCreateEffect(this);
  }
}

}
