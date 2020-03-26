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
  public void visitIBaseDefenseUCMutSetEffect(IBaseDefenseUCMutSetEffectVisitor visitor) {
    visitor.visitBaseDefenseUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseDefenseUCMutSetEffect(this);
  }
}

}
