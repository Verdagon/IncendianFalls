using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SorcerousUCDeleteEffect : ISorcerousUCEffect {
  public readonly int id;
  public SorcerousUCDeleteEffect(int id) {
    this.id = id;
  }
  int ISorcerousUCEffect.id => id;
  public void visitISorcerousUCEffect(ISorcerousUCEffectVisitor visitor) {
    visitor.visitSorcerousUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSorcerousUCEffect(this);
  }
}

}
