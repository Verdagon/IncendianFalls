using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MarkerTTCMutSetRemoveEffect : IMarkerTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MarkerTTCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMarkerTTCMutSetEffect.id => id;
  public void visit(IMarkerTTCMutSetEffectVisitor visitor) {
    visitor.visitMarkerTTCMutSetRemoveEffect(this);
  }
}

}
