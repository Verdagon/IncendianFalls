using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCMutSetAddEffect : ICounteringUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public CounteringUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ICounteringUCMutSetEffect.id => id;
  public void visitICounteringUCMutSetEffect(ICounteringUCMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCounteringUCMutSetEffect(this);
  }
}

}
