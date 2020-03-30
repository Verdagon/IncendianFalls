using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CaveTTCDeleteEffect : ICaveTTCEffect {
  public readonly int id;
  public CaveTTCDeleteEffect(int id) {
    this.id = id;
  }
  int ICaveTTCEffect.id => id;
  public void visitICaveTTCEffect(ICaveTTCEffectVisitor visitor) {
    visitor.visitCaveTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
