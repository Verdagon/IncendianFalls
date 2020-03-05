using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct NestLevelControllerDeleteEffect : INestLevelControllerEffect {
  public readonly int id;
  public NestLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int INestLevelControllerEffect.id => id;
  public void visit(INestLevelControllerEffectVisitor visitor) {
    visitor.visitNestLevelControllerDeleteEffect(this);
  }
}

}
