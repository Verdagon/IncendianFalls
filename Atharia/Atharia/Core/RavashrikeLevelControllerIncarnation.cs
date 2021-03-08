using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class RavashrikeLevelControllerIncarnation : IRavashrikeLevelControllerEffectVisitor {
  public readonly int level;
  public RavashrikeLevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public RavashrikeLevelControllerIncarnation Copy() {
    return new RavashrikeLevelControllerIncarnation(
level    );
  }

  public void visitRavashrikeLevelControllerCreateEffect(RavashrikeLevelControllerCreateEffect e) {}
  public void visitRavashrikeLevelControllerDeleteEffect(RavashrikeLevelControllerDeleteEffect e) {}

  public void ApplyEffect(IRavashrikeLevelControllerEffect effect) { effect.visitIRavashrikeLevelControllerEffect(this); }
}

}
