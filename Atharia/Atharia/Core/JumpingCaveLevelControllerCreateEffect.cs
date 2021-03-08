using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct JumpingCaveLevelControllerCreateEffect : IJumpingCaveLevelControllerEffect {
  public readonly int id;
  public readonly JumpingCaveLevelControllerIncarnation incarnation;
  public JumpingCaveLevelControllerCreateEffect(int id, JumpingCaveLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IJumpingCaveLevelControllerEffect.id => id;
  public void visitIJumpingCaveLevelControllerEffect(IJumpingCaveLevelControllerEffectVisitor visitor) {
    visitor.visitJumpingCaveLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitJumpingCaveLevelControllerEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
