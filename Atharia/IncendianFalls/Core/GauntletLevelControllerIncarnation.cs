using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GauntletLevelControllerIncarnation : IGauntletLevelControllerEffectVisitor {
  public readonly int level;
  public GauntletLevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public GauntletLevelControllerIncarnation Copy() {
    return new GauntletLevelControllerIncarnation(
level    );
  }

  public void visitGauntletLevelControllerCreateEffect(GauntletLevelControllerCreateEffect e) {}
  public void visitGauntletLevelControllerDeleteEffect(GauntletLevelControllerDeleteEffect e) {}

  public void ApplyEffect(IGauntletLevelControllerEffect effect) { effect.visitIGauntletLevelControllerEffect(this); }
}

}
