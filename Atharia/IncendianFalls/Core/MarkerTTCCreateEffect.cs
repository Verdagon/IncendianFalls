using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MarkerTTCCreateEffect : IMarkerTTCEffect {
  public readonly int id;
  public readonly MarkerTTCIncarnation incarnation;
  public MarkerTTCCreateEffect(int id, MarkerTTCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IMarkerTTCEffect.id => id;
  public void visitIMarkerTTCEffect(IMarkerTTCEffectVisitor visitor) {
    visitor.visitMarkerTTCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMarkerTTCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
