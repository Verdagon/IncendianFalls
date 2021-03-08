using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGauntletLevelControllerEffectVisitor {
  void visitGauntletLevelControllerCreateEffect(GauntletLevelControllerCreateEffect effect);
  void visitGauntletLevelControllerDeleteEffect(GauntletLevelControllerDeleteEffect effect);
}

}
