using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct IPreActingUCMutBunchDeleteEffect : IIPreActingUCMutBunchEffect {
  public readonly int id;
  public IPreActingUCMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int IIPreActingUCMutBunchEffect.id => id;
  public void visit(IIPreActingUCMutBunchEffectVisitor visitor) {
    visitor.visitIPreActingUCMutBunchDeleteEffect(this);
  }
}
       
}
