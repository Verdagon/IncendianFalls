using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CliffLevelControllerIncarnation : ICliffLevelControllerEffectVisitor {
  public readonly int level;
  public readonly int depth;
  public CliffLevelControllerIncarnation(
      int level,
      int depth) {
    this.level = level;
    this.depth = depth;
  }
  public CliffLevelControllerIncarnation Copy() {
    return new CliffLevelControllerIncarnation(
level,
depth    );
  }

  public void visitCliffLevelControllerCreateEffect(CliffLevelControllerCreateEffect e) {}
  public void visitCliffLevelControllerDeleteEffect(CliffLevelControllerDeleteEffect e) {}


  public void ApplyEffect(ICliffLevelControllerEffect effect) { effect.visitICliffLevelControllerEffect(this); }
}

}
