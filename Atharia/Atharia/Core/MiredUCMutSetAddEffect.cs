using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MiredUCMutSetAddEffect : IMiredUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MiredUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMiredUCMutSetEffect.id => id;
  public void visitIMiredUCMutSetEffect(IMiredUCMutSetEffectVisitor visitor) {
    visitor.visitMiredUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMiredUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
