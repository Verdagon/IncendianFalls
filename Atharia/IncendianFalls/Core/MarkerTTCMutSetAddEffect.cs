using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MarkerTTCMutSetAddEffect : IMarkerTTCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public MarkerTTCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int IMarkerTTCMutSetEffect.id => id;
  public void visit(IMarkerTTCMutSetEffectVisitor visitor) {
    visitor.visitMarkerTTCMutSetAddEffect(this);
  }
}

}
