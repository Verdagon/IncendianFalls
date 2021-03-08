using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct FallsTTCDeleteEffect : IFallsTTCEffect {
  public readonly int id;
  public FallsTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IFallsTTCEffect.id => id;
  public void visitIFallsTTCEffect(IFallsTTCEffectVisitor visitor) {
    visitor.visitFallsTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFallsTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
