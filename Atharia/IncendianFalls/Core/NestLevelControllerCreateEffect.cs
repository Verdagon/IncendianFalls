using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct NestLevelControllerCreateEffect : INestLevelControllerEffect {
  public readonly int id;
  public NestLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int INestLevelControllerEffect.id => id;
  public void visit(INestLevelControllerEffectVisitor visitor) {
    visitor.visitNestLevelControllerCreateEffect(this);
  }
}

}
