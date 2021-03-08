using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class JumpingCaveLevelControllerIncarnation : IJumpingCaveLevelControllerEffectVisitor {
  public readonly int level;
  public readonly int depth;
  public JumpingCaveLevelControllerIncarnation(
      int level,
      int depth) {
    this.level = level;
    this.depth = depth;
  }
  public JumpingCaveLevelControllerIncarnation Copy() {
    return new JumpingCaveLevelControllerIncarnation(
level,
depth    );
  }

  public void visitJumpingCaveLevelControllerCreateEffect(JumpingCaveLevelControllerCreateEffect e) {}
  public void visitJumpingCaveLevelControllerDeleteEffect(JumpingCaveLevelControllerDeleteEffect e) {}


  public void ApplyEffect(IJumpingCaveLevelControllerEffect effect) { effect.visitIJumpingCaveLevelControllerEffect(this); }
}

}
