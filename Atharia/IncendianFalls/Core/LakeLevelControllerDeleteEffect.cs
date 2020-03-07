using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct LakeLevelControllerDeleteEffect : ILakeLevelControllerEffect {
  public readonly int id;
  public LakeLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int ILakeLevelControllerEffect.id => id;
  public void visit(ILakeLevelControllerEffectVisitor visitor) {
    visitor.visitLakeLevelControllerDeleteEffect(this);
  }
}

}
