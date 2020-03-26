using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CaveLevelControllerDeleteEffect : ICaveLevelControllerEffect {
  public readonly int id;
  public CaveLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int ICaveLevelControllerEffect.id => id;
  public void visitICaveLevelControllerEffect(ICaveLevelControllerEffectVisitor visitor) {
    visitor.visitCaveLevelControllerDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveLevelControllerEffect(this);
  }
}

}
