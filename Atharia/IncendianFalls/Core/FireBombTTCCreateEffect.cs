using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FireBombTTCCreateEffect : IFireBombTTCEffect {
  public readonly int id;
  public readonly FireBombTTCIncarnation incarnation;
  public FireBombTTCCreateEffect(int id, FireBombTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IFireBombTTCEffect.id => id;
  public void visitIFireBombTTCEffect(IFireBombTTCEffectVisitor visitor) {
    visitor.visitFireBombTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFireBombTTCEffect(this);
  }
}

}
