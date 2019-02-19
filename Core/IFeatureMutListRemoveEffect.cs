using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IFeatureMutListRemoveEffect : IIFeatureMutListEffect {
  public readonly int id;
  public readonly int elementId;
  public IFeatureMutListRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IIFeatureMutListEffect.id => id;
  public void visit(IIFeatureMutListEffectVisitor visitor) {
    visitor.visitIFeatureMutListRemoveEffect(this);
  }
}

}
