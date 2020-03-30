using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IRequestMutListCreateEffect : IIRequestMutListEffect {
  public readonly int id;
  public IRequestMutListCreateEffect(int id) {
    this.id = id;
  }
  int IIRequestMutListEffect.id => id;
  public void visitIIRequestMutListEffect(IIRequestMutListEffectVisitor visitor) {
    visitor.visitIRequestMutListCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIRequestMutListEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
