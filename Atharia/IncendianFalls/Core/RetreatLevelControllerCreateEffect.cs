using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct RetreatLevelControllerCreateEffect : IRetreatLevelControllerEffect {
  public readonly int id;
  public readonly RetreatLevelControllerIncarnation incarnation;
  public RetreatLevelControllerCreateEffect(int id, RetreatLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IRetreatLevelControllerEffect.id => id;
  public void visitIRetreatLevelControllerEffect(IRetreatLevelControllerEffectVisitor visitor) {
    visitor.visitRetreatLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitRetreatLevelControllerEffect(this);
  }
}

}
