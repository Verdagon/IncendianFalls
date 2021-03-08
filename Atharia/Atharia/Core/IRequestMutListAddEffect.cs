using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IRequestMutListAddEffect : IIRequestMutListEffect {
  public readonly int id;
  public readonly int index;
  public readonly IRequest element;
  public IRequestMutListAddEffect(int id, int index, IRequest element) {
    this.id = id;
    this.index = index;
    this.element = element;
  }
  int IIRequestMutListEffect.id => id;
  public void visitIIRequestMutListEffect(IIRequestMutListEffectVisitor visitor) {
    visitor.visitIRequestMutListAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitIRequestMutListEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
