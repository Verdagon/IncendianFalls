using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MiredUCWeakMutSetCreateEffect : IMiredUCWeakMutSetEffect {
  public readonly int id;
  public MiredUCWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IMiredUCWeakMutSetEffect.id => id;
  public void visitIMiredUCWeakMutSetEffect(IMiredUCWeakMutSetEffectVisitor visitor) {
    visitor.visitMiredUCWeakMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMiredUCWeakMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
