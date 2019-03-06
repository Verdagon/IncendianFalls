using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IRidgeLevelControllerEffectVisitor {
  void visitRidgeLevelControllerCreateEffect(RidgeLevelControllerCreateEffect effect);
  void visitRidgeLevelControllerDeleteEffect(RidgeLevelControllerDeleteEffect effect);
}

}
