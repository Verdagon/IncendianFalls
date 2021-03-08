using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class PreGauntletLevelControllerIncarnation : IPreGauntletLevelControllerEffectVisitor {
  public readonly int level;
  public PreGauntletLevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public PreGauntletLevelControllerIncarnation Copy() {
    return new PreGauntletLevelControllerIncarnation(
level    );
  }

  public void visitPreGauntletLevelControllerCreateEffect(PreGauntletLevelControllerCreateEffect e) {}
  public void visitPreGauntletLevelControllerDeleteEffect(PreGauntletLevelControllerDeleteEffect e) {}

  public void ApplyEffect(IPreGauntletLevelControllerEffect effect) { effect.visitIPreGauntletLevelControllerEffect(this); }
}

}
