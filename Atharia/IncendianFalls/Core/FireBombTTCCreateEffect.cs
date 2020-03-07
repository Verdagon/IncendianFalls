using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireBombTTCCreateEffect : IFireBombTTCEffect {
  public readonly int id;
  public FireBombTTCCreateEffect(int id) {
    this.id = id;
  }
  int IFireBombTTCEffect.id => id;
  public void visit(IFireBombTTCEffectVisitor visitor) {
    visitor.visitFireBombTTCCreateEffect(this);
  }
}

}
