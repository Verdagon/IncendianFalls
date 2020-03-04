using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct MarkerTTCCreateEffect : IMarkerTTCEffect {
  public readonly int id;
  public MarkerTTCCreateEffect(int id) {
    this.id = id;
  }
  int IMarkerTTCEffect.id => id;
  public void visit(IMarkerTTCEffectVisitor visitor) {
    visitor.visitMarkerTTCCreateEffect(this);
  }
}

}
