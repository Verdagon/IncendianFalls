using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct PreGauntletLevelControllerDeleteEffect : IPreGauntletLevelControllerEffect {
  public readonly int id;
  public PreGauntletLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int IPreGauntletLevelControllerEffect.id => id;
  public void visit(IPreGauntletLevelControllerEffectVisitor visitor) {
    visitor.visitPreGauntletLevelControllerDeleteEffect(this);
  }
}

}
