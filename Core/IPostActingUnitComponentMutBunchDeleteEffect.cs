using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IPostActingUnitComponentMutBunchDeleteEffect : IIPostActingUnitComponentMutBunchEffect {
  public readonly int id;
  public IPostActingUnitComponentMutBunchDeleteEffect(int id) {
    this.id = id;
  }
  int IIPostActingUnitComponentMutBunchEffect.id => id;
  public void visit(IIPostActingUnitComponentMutBunchEffectVisitor visitor) {
    visitor.visitIPostActingUnitComponentMutBunchDeleteEffect(this);
  }
}
       
}
