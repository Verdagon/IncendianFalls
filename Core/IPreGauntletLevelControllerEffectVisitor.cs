using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IPreGauntletLevelControllerEffectVisitor {
  void visitPreGauntletLevelControllerCreateEffect(PreGauntletLevelControllerCreateEffect effect);
  void visitPreGauntletLevelControllerDeleteEffect(PreGauntletLevelControllerDeleteEffect effect);
}

}
