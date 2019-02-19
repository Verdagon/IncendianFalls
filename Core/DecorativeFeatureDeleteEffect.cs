using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct DecorativeFeatureDeleteEffect : IDecorativeFeatureEffect {
  public readonly int id;
  public DecorativeFeatureDeleteEffect(int id) {
    this.id = id;
  }
  int IDecorativeFeatureEffect.id => id;
  public void visit(IDecorativeFeatureEffectVisitor visitor) {
    visitor.visitDecorativeFeatureDeleteEffect(this);
  }
}
       
}
