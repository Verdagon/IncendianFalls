using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BridgesLevelControllerCreateEffect : IBridgesLevelControllerEffect {
  public readonly int id;
  public readonly BridgesLevelControllerIncarnation incarnation;
  public BridgesLevelControllerCreateEffect(int id, BridgesLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBridgesLevelControllerEffect.id => id;
  public void visitIBridgesLevelControllerEffect(IBridgesLevelControllerEffectVisitor visitor) {
    visitor.visitBridgesLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBridgesLevelControllerEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
