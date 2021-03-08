using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class VolcaetusLevelControllerIncarnation : IVolcaetusLevelControllerEffectVisitor {
  public readonly int level;
  public VolcaetusLevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public VolcaetusLevelControllerIncarnation Copy() {
    return new VolcaetusLevelControllerIncarnation(
level    );
  }

  public void visitVolcaetusLevelControllerCreateEffect(VolcaetusLevelControllerCreateEffect e) {}
  public void visitVolcaetusLevelControllerDeleteEffect(VolcaetusLevelControllerDeleteEffect e) {}

  public void ApplyEffect(IVolcaetusLevelControllerEffect effect) { effect.visitIVolcaetusLevelControllerEffect(this); }
}

}
