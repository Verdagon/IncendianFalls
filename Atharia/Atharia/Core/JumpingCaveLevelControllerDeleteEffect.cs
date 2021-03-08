using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct JumpingCaveLevelControllerDeleteEffect : IJumpingCaveLevelControllerEffect {
  public readonly int id;
  public JumpingCaveLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int IJumpingCaveLevelControllerEffect.id => id;
  public void visitIJumpingCaveLevelControllerEffect(IJumpingCaveLevelControllerEffectVisitor visitor) {
    visitor.visitJumpingCaveLevelControllerDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitJumpingCaveLevelControllerEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
