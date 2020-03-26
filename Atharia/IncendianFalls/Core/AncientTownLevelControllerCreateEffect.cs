using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct AncientTownLevelControllerCreateEffect : IAncientTownLevelControllerEffect {
  public readonly int id;
  public readonly AncientTownLevelControllerIncarnation incarnation;
  public AncientTownLevelControllerCreateEffect(int id, AncientTownLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IAncientTownLevelControllerEffect.id => id;
  public void visitIAncientTownLevelControllerEffect(IAncientTownLevelControllerEffectVisitor visitor) {
    visitor.visitAncientTownLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitAncientTownLevelControllerEffect(this);
  }
}

}
