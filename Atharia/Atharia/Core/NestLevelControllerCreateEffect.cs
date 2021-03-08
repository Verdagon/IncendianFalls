using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct NestLevelControllerCreateEffect : INestLevelControllerEffect {
  public readonly int id;
  public readonly NestLevelControllerIncarnation incarnation;
  public NestLevelControllerCreateEffect(int id, NestLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int INestLevelControllerEffect.id => id;
  public void visitINestLevelControllerEffect(INestLevelControllerEffectVisitor visitor) {
    visitor.visitNestLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitNestLevelControllerEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
