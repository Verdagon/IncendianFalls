using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class LakeLevelControllerIncarnation : ILakeLevelControllerEffectVisitor {
  public readonly int level;
  public LakeLevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public LakeLevelControllerIncarnation Copy() {
    return new LakeLevelControllerIncarnation(
level    );
  }

  public void visitLakeLevelControllerCreateEffect(LakeLevelControllerCreateEffect e) {}
  public void visitLakeLevelControllerDeleteEffect(LakeLevelControllerDeleteEffect e) {}

  public void ApplyEffect(ILakeLevelControllerEffect effect) { effect.visitILakeLevelControllerEffect(this); }
}

}
