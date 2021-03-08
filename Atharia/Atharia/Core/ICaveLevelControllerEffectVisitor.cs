using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICaveLevelControllerEffectVisitor {
  void visitCaveLevelControllerCreateEffect(CaveLevelControllerCreateEffect effect);
  void visitCaveLevelControllerDeleteEffect(CaveLevelControllerDeleteEffect effect);
}

}
