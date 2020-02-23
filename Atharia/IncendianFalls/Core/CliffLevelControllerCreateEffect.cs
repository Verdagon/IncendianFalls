using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CliffLevelControllerCreateEffect : ICliffLevelControllerEffect {
  public readonly int id;
  public CliffLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int ICliffLevelControllerEffect.id => id;
  public void visit(ICliffLevelControllerEffectVisitor visitor) {
    visitor.visitCliffLevelControllerCreateEffect(this);
  }
}

}
