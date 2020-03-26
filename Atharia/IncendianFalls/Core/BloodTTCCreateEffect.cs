using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct BloodTTCCreateEffect : IBloodTTCEffect {
  public readonly int id;
  public readonly BloodTTCIncarnation incarnation;
  public BloodTTCCreateEffect(int id, BloodTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IBloodTTCEffect.id => id;
  public void visitIBloodTTCEffect(IBloodTTCEffectVisitor visitor) {
    visitor.visitBloodTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitBloodTTCEffect(this);
  }
}

}
