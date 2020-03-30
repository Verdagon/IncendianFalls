using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BridgesLevelControllerDeleteEffect : IBridgesLevelControllerEffect {
  public readonly int id;
  public BridgesLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int IBridgesLevelControllerEffect.id => id;
  public void visitIBridgesLevelControllerEffect(IBridgesLevelControllerEffectVisitor visitor) {
    visitor.visitBridgesLevelControllerDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBridgesLevelControllerEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
