using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FloorTTCMutSetAddEffect : IFloorTTCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public FloorTTCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IFloorTTCMutSetEffect.id => id;
  public void visitIFloorTTCMutSetEffect(IFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitFloorTTCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitFloorTTCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
