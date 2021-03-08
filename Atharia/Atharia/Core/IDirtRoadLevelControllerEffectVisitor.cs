using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDirtRoadLevelControllerEffectVisitor {
  void visitDirtRoadLevelControllerCreateEffect(DirtRoadLevelControllerCreateEffect effect);
  void visitDirtRoadLevelControllerDeleteEffect(DirtRoadLevelControllerDeleteEffect effect);
}

}
