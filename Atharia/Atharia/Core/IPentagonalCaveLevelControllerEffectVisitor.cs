using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IPentagonalCaveLevelControllerEffectVisitor {
  void visitPentagonalCaveLevelControllerCreateEffect(PentagonalCaveLevelControllerCreateEffect effect);
  void visitPentagonalCaveLevelControllerDeleteEffect(PentagonalCaveLevelControllerDeleteEffect effect);
}

}
