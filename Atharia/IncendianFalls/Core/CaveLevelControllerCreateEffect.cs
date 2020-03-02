using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CaveLevelControllerCreateEffect : ICaveLevelControllerEffect {
  public readonly int id;
  public CaveLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int ICaveLevelControllerEffect.id => id;
  public void visit(ICaveLevelControllerEffectVisitor visitor) {
    visitor.visitCaveLevelControllerCreateEffect(this);
  }
}

}
