using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IJumpingCaveLevelControllerEffectVisitor {
  void visitJumpingCaveLevelControllerCreateEffect(JumpingCaveLevelControllerCreateEffect effect);
  void visitJumpingCaveLevelControllerDeleteEffect(JumpingCaveLevelControllerDeleteEffect effect);
}

}
