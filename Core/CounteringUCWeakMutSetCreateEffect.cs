using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct CounteringUCWeakMutSetCreateEffect : ICounteringUCWeakMutSetEffect {
  public readonly int id;
  public CounteringUCWeakMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ICounteringUCWeakMutSetEffect.id => id;
  public void visit(ICounteringUCWeakMutSetEffectVisitor visitor) {
    visitor.visitCounteringUCWeakMutSetCreateEffect(this);
  }
}

}
