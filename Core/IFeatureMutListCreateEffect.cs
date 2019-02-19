using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct IFeatureMutListCreateEffect : IIFeatureMutListEffect {
  public readonly int id;
  public readonly IFeatureMutListIncarnation incarnation;
  public IFeatureMutListCreateEffect(
      int id,
      IFeatureMutListIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IIFeatureMutListEffect.id => id;
  public void visit(IIFeatureMutListEffectVisitor visitor) {
    visitor.visitIFeatureMutListCreateEffect(this);
  }
}

}
