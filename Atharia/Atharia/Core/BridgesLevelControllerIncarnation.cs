using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BridgesLevelControllerIncarnation : IBridgesLevelControllerEffectVisitor {
  public readonly int level;
  public BridgesLevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public BridgesLevelControllerIncarnation Copy() {
    return new BridgesLevelControllerIncarnation(
level    );
  }

  public void visitBridgesLevelControllerCreateEffect(BridgesLevelControllerCreateEffect e) {}
  public void visitBridgesLevelControllerDeleteEffect(BridgesLevelControllerDeleteEffect e) {}

  public void ApplyEffect(IBridgesLevelControllerEffect effect) { effect.visitIBridgesLevelControllerEffect(this); }
}

}
