using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct WatDeleteEffect : IWatEffect {
  public readonly int id;
  public WatDeleteEffect(int id) {
    this.id = id;
  }
  int IWatEffect.id => id;
  public void visit(IWatEffectVisitor visitor) {
    visitor.visitWatDeleteEffect(this);
  }
}

}
