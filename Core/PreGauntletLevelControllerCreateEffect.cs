using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct PreGauntletLevelControllerCreateEffect : IPreGauntletLevelControllerEffect {
  public readonly int id;
  public PreGauntletLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int IPreGauntletLevelControllerEffect.id => id;
  public void visit(IPreGauntletLevelControllerEffectVisitor visitor) {
    visitor.visitPreGauntletLevelControllerCreateEffect(this);
  }
}

}
