using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GauntletLevelControllerDeleteEffect : IGauntletLevelControllerEffect {
  public readonly int id;
  public GauntletLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int IGauntletLevelControllerEffect.id => id;
  public void visit(IGauntletLevelControllerEffectVisitor visitor) {
    visitor.visitGauntletLevelControllerDeleteEffect(this);
  }
}

}