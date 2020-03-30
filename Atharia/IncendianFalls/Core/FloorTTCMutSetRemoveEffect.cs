using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FloorTTCMutSetRemoveEffect : IFloorTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FloorTTCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFloorTTCMutSetEffect.id => id;
  public void visitIFloorTTCMutSetEffect(IFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitFloorTTCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFloorTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
