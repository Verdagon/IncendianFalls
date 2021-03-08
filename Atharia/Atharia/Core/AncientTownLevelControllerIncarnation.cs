using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class AncientTownLevelControllerIncarnation : IAncientTownLevelControllerEffectVisitor {
  public readonly int level;
  public AncientTownLevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public AncientTownLevelControllerIncarnation Copy() {
    return new AncientTownLevelControllerIncarnation(
level    );
  }

  public void visitAncientTownLevelControllerCreateEffect(AncientTownLevelControllerCreateEffect e) {}
  public void visitAncientTownLevelControllerDeleteEffect(AncientTownLevelControllerDeleteEffect e) {}

  public void ApplyEffect(IAncientTownLevelControllerEffect effect) { effect.visitIAncientTownLevelControllerEffect(this); }
}

}
