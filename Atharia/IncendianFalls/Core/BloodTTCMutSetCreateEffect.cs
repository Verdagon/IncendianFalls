using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct BloodTTCMutSetCreateEffect : IBloodTTCMutSetEffect {
  public readonly int id;
  public BloodTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IBloodTTCMutSetEffect.id => id;
  public void visit(IBloodTTCMutSetEffectVisitor visitor) {
    visitor.visitBloodTTCMutSetCreateEffect(this);
  }
}

}
