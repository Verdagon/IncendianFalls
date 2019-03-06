using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICliffLevelControllerEffectVisitor {
  void visitCliffLevelControllerCreateEffect(CliffLevelControllerCreateEffect effect);
  void visitCliffLevelControllerDeleteEffect(CliffLevelControllerDeleteEffect effect);
}

}
