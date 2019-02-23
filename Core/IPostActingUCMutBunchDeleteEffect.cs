using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IPostActingUCMutBunchDeleteEffect : IIPostActingUCMutBunchEffect {
  public readonly int id;
  public IPostActingUCMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int IIPostActingUCMutBunchEffect.id => id;
  public void visit(IIPostActingUCMutBunchEffectVisitor visitor) {
    visitor.visitIPostActingUCMutBunchDeleteEffect(this);
  }
}
       
}
