using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IIslandLevelControllerEffectVisitor {
  void visitIslandLevelControllerCreateEffect(IslandLevelControllerCreateEffect effect);
  void visitIslandLevelControllerDeleteEffect(IslandLevelControllerDeleteEffect effect);
}

}
