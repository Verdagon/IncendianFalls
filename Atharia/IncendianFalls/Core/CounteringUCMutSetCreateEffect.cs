using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCMutSetCreateEffect : ICounteringUCMutSetEffect {
  public readonly int id;
  public CounteringUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ICounteringUCMutSetEffect.id => id;
  public void visit(ICounteringUCMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCMutSetCreateEffect(this);
  }
}

}
