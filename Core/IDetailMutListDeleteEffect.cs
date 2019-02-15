using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IDetailMutListDeleteEffect : IIDetailMutListEffect {
  public readonly int id;
  public IDetailMutListDeleteEffect(int id) {
    this.id = id;
  }
  int IIDetailMutListEffect.id => id;
  public void visit(IIDetailMutListEffectVisitor visitor) {
    visitor.visitIDetailMutListDeleteEffect(this);
  }
}

}
