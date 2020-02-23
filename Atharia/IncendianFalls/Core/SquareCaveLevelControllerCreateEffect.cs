using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SquareCaveLevelControllerCreateEffect : ISquareCaveLevelControllerEffect {
  public readonly int id;
  public SquareCaveLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int ISquareCaveLevelControllerEffect.id => id;
  public void visit(ISquareCaveLevelControllerEffectVisitor visitor) {
    visitor.visitSquareCaveLevelControllerCreateEffect(this);
  }
}

}
