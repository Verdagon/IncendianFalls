using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IFeatureMutListDeleteEffect : IIFeatureMutListEffect {
  public readonly int id;
  public IFeatureMutListDeleteEffect(int id) {
    this.id = id;
  }
  int IIFeatureMutListEffect.id => id;
  public void visit(IIFeatureMutListEffectVisitor visitor) {
    visitor.visitIFeatureMutListDeleteEffect(this);
  }
}

}
