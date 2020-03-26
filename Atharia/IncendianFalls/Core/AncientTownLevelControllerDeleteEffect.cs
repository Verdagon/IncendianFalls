using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct AncientTownLevelControllerDeleteEffect : IAncientTownLevelControllerEffect {
  public readonly int id;
  public AncientTownLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int IAncientTownLevelControllerEffect.id => id;
  public void visitIAncientTownLevelControllerEffect(IAncientTownLevelControllerEffectVisitor visitor) {
    visitor.visitAncientTownLevelControllerDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAncientTownLevelControllerEffect(this);
  }
}

}
