using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MiredUCMutSetCreateEffect : IMiredUCMutSetEffect {
  public readonly int id;
  public MiredUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IMiredUCMutSetEffect.id => id;
  public void visitIMiredUCMutSetEffect(IMiredUCMutSetEffectVisitor visitor) {
    visitor.visitMiredUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMiredUCMutSetEffect(this);
  }
}

}
