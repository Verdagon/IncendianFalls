using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WatCreateEffect : IWatEffect {
  public readonly int id;
  public readonly WatIncarnation incarnation;
  public WatCreateEffect(int id, WatIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IWatEffect.id => id;
  public void visitIWatEffect(IWatEffectVisitor visitor) {
    visitor.visitWatCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitWatEffect(this);
  }
}

}
