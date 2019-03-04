using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IRequestMutListAddEffect : IIRequestMutListEffect {
  public readonly int id;
  public readonly IRequest element;
  public IRequestMutListAddEffect(int id, IRequest element) {
    this.id = id;
    this.element = element;
  }
  int IIRequestMutListEffect.id => id;
  public void visit(IIRequestMutListEffectVisitor visitor) {
    visitor.visitIRequestMutListAddEffect(this);
  }
}

}
