using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BridgesLevelControllerCreateEffect : IBridgesLevelControllerEffect {
  public readonly int id;
  public BridgesLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int IBridgesLevelControllerEffect.id => id;
  public void visit(IBridgesLevelControllerEffectVisitor visitor) {
    visitor.visitBridgesLevelControllerCreateEffect(this);
  }
}

}
