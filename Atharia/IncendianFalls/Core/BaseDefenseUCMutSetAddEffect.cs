using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BaseDefenseUCMutSetAddEffect : IBaseDefenseUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public BaseDefenseUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IBaseDefenseUCMutSetEffect.id => id;
  public void visitIBaseDefenseUCMutSetEffect(IBaseDefenseUCMutSetEffectVisitor visitor) {
    visitor.visitBaseDefenseUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBaseDefenseUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
