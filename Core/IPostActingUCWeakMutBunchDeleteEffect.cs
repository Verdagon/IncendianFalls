using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IPostActingUCWeakMutBunchDeleteEffect : IIPostActingUCWeakMutBunchEffect {
  public readonly int id;
  public IPostActingUCWeakMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int IIPostActingUCWeakMutBunchEffect.id => id;
  public void visit(IIPostActingUCWeakMutBunchEffectVisitor visitor) {
    visitor.visitIPostActingUCWeakMutBunchDeleteEffect(this);
  }
}

}
