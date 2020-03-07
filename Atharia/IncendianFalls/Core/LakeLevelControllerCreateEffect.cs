using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LakeLevelControllerCreateEffect : ILakeLevelControllerEffect {
  public readonly int id;
  public LakeLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int ILakeLevelControllerEffect.id => id;
  public void visit(ILakeLevelControllerEffectVisitor visitor) {
    visitor.visitLakeLevelControllerCreateEffect(this);
  }
}

}
