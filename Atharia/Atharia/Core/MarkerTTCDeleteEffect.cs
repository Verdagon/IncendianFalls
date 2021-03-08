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
  public void visitIMarkerTTCEffect(IMarkerTTCEffectVisitor visitor) {
    visitor.visitMarkerTTCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMarkerTTCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
