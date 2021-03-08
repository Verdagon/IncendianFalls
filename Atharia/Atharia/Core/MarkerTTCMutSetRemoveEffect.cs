using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MarkerTTCMutSetRemoveEffect : IMarkerTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public MarkerTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IMarkerTTCMutSetEffect.id => id;
  public void visitIMarkerTTCMutSetEffect(IMarkerTTCMutSetEffectVisitor visitor) {
    visitor.visitMarkerTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMarkerTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
