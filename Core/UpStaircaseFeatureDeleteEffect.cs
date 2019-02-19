using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct UpStaircaseFeatureDeleteEffect : IUpStaircaseFeatureEffect {
  public readonly int id;
  public UpStaircaseFeatureDeleteEffect(int id) {
    this.id = id;
  }
  int IUpStaircaseFeatureEffect.id => id;
  public void visit(IUpStaircaseFeatureEffectVisitor visitor) {
    visitor.visitUpStaircaseFeatureDeleteEffect(this);
  }
}
       
}
