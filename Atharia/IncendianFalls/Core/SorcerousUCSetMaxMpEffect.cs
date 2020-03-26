using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SorcerousUCSetMaxMpEffect : ISorcerousUCEffect {
  public readonly int id;
  public readonly int newValue;
  public SorcerousUCSetMaxMpEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int ISorcerousUCEffect.id => id;

  public void visitISorcerousUCEffect(ISorcerousUCEffectVisitor visitor) {
    visitor.visitSorcerousUCSetMaxMpEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitSorcerousUCEffect(this);
  }
}

}
