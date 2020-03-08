using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RetreatLevelControllerCreateEffect : IRetreatLevelControllerEffect {
  public readonly int id;
  public RetreatLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int IRetreatLevelControllerEffect.id => id;
  public void visit(IRetreatLevelControllerEffectVisitor visitor) {
    visitor.visitRetreatLevelControllerCreateEffect(this);
  }
}

}
