using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct SorcerousUCCreateEffect : ISorcerousUCEffect {
  public readonly int id;
  public SorcerousUCCreateEffect(int id) {
    this.id = id;
  }
  int ISorcerousUCEffect.id => id;
  public void visit(ISorcerousUCEffectVisitor visitor) {
    visitor.visitSorcerousUCCreateEffect(this);
  }
}

}
