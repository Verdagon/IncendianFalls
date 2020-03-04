using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MarkerTTCMutSetDeleteEffect : IMarkerTTCMutSetEffect {
  public readonly int id;
  public MarkerTTCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IMarkerTTCMutSetEffect.id => id;
  public void visit(IMarkerTTCMutSetEffectVisitor visitor) {
    visitor.visitMarkerTTCMutSetDeleteEffect(this);
  }
}

}
