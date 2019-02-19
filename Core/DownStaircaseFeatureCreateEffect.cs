using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct DownStaircaseFeatureCreateEffect : IDownStaircaseFeatureEffect {
  public readonly int id;
  public readonly DownStaircaseFeatureIncarnation incarnation;
  public DownStaircaseFeatureCreateEffect(
      int id,
      DownStaircaseFeatureIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IDownStaircaseFeatureEffect.id => id;
  public void visit(IDownStaircaseFeatureEffectVisitor visitor) {
    visitor.visitDownStaircaseFeatureCreateEffect(this);
  }
}
       
}
