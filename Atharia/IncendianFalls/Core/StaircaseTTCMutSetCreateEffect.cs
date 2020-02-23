using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct StaircaseTTCMutSetCreateEffect : IStaircaseTTCMutSetEffect {
  public readonly int id;
  public StaircaseTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IStaircaseTTCMutSetEffect.id => id;
  public void visit(IStaircaseTTCMutSetEffectVisitor visitor) {
    visitor.visitStaircaseTTCMutSetCreateEffect(this);
  }
}

}
