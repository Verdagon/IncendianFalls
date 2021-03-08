using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RetreatLevelControllerIncarnation : IRetreatLevelControllerEffectVisitor {
  public readonly int level;
  public RetreatLevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public RetreatLevelControllerIncarnation Copy() {
    return new RetreatLevelControllerIncarnation(
level    );
  }

  public void visitRetreatLevelControllerCreateEffect(RetreatLevelControllerCreateEffect e) {}
  public void visitRetreatLevelControllerDeleteEffect(RetreatLevelControllerDeleteEffect e) {}

  public void ApplyEffect(IRetreatLevelControllerEffect effect) { effect.visitIRetreatLevelControllerEffect(this); }
}

}
