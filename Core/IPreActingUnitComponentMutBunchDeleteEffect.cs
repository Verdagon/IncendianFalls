using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IPreActingUnitComponentMutBunchDeleteEffect : IIPreActingUnitComponentMutBunchEffect {
  public readonly int id;
  public IPreActingUnitComponentMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int IIPreActingUnitComponentMutBunchEffect.id => id;
  public void visit(IIPreActingUnitComponentMutBunchEffectVisitor visitor) {
    visitor.visitIPreActingUnitComponentMutBunchDeleteEffect(this);
  }
}
       
}
