using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MarkerTTCMutSetAddEffect : IMarkerTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MarkerTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMarkerTTCMutSetEffect.id => id;
  public void visitIMarkerTTCMutSetEffect(IMarkerTTCMutSetEffectVisitor visitor) {
    visitor.visitMarkerTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMarkerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
