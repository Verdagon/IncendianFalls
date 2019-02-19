using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct DownStaircaseFeatureDeleteEffect : IDownStaircaseFeatureEffect {
  public readonly int id;
  public DownStaircaseFeatureDeleteEffect(int id) {
    this.id = id;
  }
  int IDownStaircaseFeatureEffect.id => id;
  public void visit(IDownStaircaseFeatureEffectVisitor visitor) {
    visitor.visitDownStaircaseFeatureDeleteEffect(this);
  }
}
       
}
