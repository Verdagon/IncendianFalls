using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IRequestMutListDeleteEffect : IIRequestMutListEffect {
  public readonly int id;
  public IRequestMutListDeleteEffect(int id) {
    this.id = id;
  }
  int IIRequestMutListEffect.id => id;
  public void visit(IIRequestMutListEffectVisitor visitor) {
    visitor.visitIRequestMutListDeleteEffect(this);
  }
}

}