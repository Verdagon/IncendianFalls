using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IDetailMutListAddEffect : IIDetailMutListEffect {
  public readonly int id;
  public readonly int element;
  public IDetailMutListAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IIDetailMutListEffect.id => id;
  public void visit(IIDetailMutListEffectVisitor visitor) {
    visitor.visitIDetailMutListAddEffect(this);
  }
}

}
