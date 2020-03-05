using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseDefenseUCMutSetRemoveEffect : IBaseDefenseUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public BaseDefenseUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IBaseDefenseUCMutSetEffect.id => id;
  public void visit(IBaseDefenseUCMutSetEffectVisitor visitor) {
    visitor.visitBaseDefenseUCMutSetRemoveEffect(this);
  }
}

}
