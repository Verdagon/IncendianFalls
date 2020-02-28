using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WatCreateEffect : IWatEffect {
  public readonly int id;
  public WatCreateEffect(int id) {
    this.id = id;
  }
  int IWatEffect.id => id;
  public void visit(IWatEffectVisitor visitor) {
    visitor.visitWatCreateEffect(this);
  }
}

}
