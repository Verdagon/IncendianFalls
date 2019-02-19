using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct UpStaircaseFeatureCreateEffect : IUpStaircaseFeatureEffect {
  public readonly int id;
  public readonly UpStaircaseFeatureIncarnation incarnation;
  public UpStaircaseFeatureCreateEffect(
      int id,
      UpStaircaseFeatureIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IUpStaircaseFeatureEffect.id => id;
  public void visit(IUpStaircaseFeatureEffectVisitor visitor) {
    visitor.visitUpStaircaseFeatureCreateEffect(this);
  }
}
       
}
