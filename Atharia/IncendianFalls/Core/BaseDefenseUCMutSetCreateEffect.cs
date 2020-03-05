using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseDefenseUCMutSetCreateEffect : IBaseDefenseUCMutSetEffect {
  public readonly int id;
  public BaseDefenseUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBaseDefenseUCMutSetEffect.id => id;
  public void visit(IBaseDefenseUCMutSetEffectVisitor visitor) {
    visitor.visitBaseDefenseUCMutSetCreateEffect(this);
  }
}

}
