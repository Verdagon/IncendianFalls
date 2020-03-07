using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct AncientTownLevelControllerCreateEffect : IAncientTownLevelControllerEffect {
  public readonly int id;
  public AncientTownLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int IAncientTownLevelControllerEffect.id => id;
  public void visit(IAncientTownLevelControllerEffectVisitor visitor) {
    visitor.visitAncientTownLevelControllerCreateEffect(this);
  }
}

}
