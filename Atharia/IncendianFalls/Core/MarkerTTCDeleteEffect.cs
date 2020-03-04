using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MarkerTTCDeleteEffect : IMarkerTTCEffect {
  public readonly int id;
  public MarkerTTCDeleteEffect(int id) {
    this.id = id;
  }
  int IMarkerTTCEffect.id => id;
  public void visit(IMarkerTTCEffectVisitor visitor) {
    visitor.visitMarkerTTCDeleteEffect(this);
  }
}

}
