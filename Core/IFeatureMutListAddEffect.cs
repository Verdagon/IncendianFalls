using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IFeatureMutListAddEffect : IIFeatureMutListEffect {
  public readonly int id;
  public readonly int element;
  public IFeatureMutListAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IIFeatureMutListEffect.id => id;
  public void visit(IIFeatureMutListEffectVisitor visitor) {
    visitor.visitIFeatureMutListAddEffect(this);
  }
}

}
