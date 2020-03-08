using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FireTTCMutSetCreateEffect : IFireTTCMutSetEffect {
  public readonly int id;
  public FireTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IFireTTCMutSetEffect.id => id;
  public void visit(IFireTTCMutSetEffectVisitor visitor) {
    visitor.visitFireTTCMutSetCreateEffect(this);
  }
}

}
