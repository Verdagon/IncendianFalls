using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseOffenseUCMutSetAddEffect : IBaseOffenseUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BaseOffenseUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBaseOffenseUCMutSetEffect.id => id;
  public void visit(IBaseOffenseUCMutSetEffectVisitor visitor) {
    visitor.visitBaseOffenseUCMutSetAddEffect(this);
  }
}

}
