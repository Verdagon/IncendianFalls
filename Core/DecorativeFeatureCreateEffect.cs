using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct DecorativeFeatureCreateEffect : IDecorativeFeatureEffect {
  public readonly int id;
  public readonly DecorativeFeatureIncarnation incarnation;
  public DecorativeFeatureCreateEffect(
      int id,
      DecorativeFeatureIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDecorativeFeatureEffect.id => id;
  public void visit(IDecorativeFeatureEffectVisitor visitor) {
    visitor.visitDecorativeFeatureCreateEffect(this);
  }
}
       
}
