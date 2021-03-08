using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBridgesLevelControllerEffectVisitor {
  void visitBridgesLevelControllerCreateEffect(BridgesLevelControllerCreateEffect effect);
  void visitBridgesLevelControllerDeleteEffect(BridgesLevelControllerDeleteEffect effect);
}

}
