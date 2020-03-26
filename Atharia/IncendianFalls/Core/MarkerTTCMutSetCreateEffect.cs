using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct MarkerTTCMutSetCreateEffect : IMarkerTTCMutSetEffect {
  public readonly int id;
  public MarkerTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IMarkerTTCMutSetEffect.id => id;
  public void visitIMarkerTTCMutSetEffect(IMarkerTTCMutSetEffectVisitor visitor) {
    visitor.visitMarkerTTCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitMarkerTTCMutSetEffect(this);
  }
}

}
