using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct FloorTTCMutSetCreateEffect : IFloorTTCMutSetEffect {
  public readonly int id;
  public FloorTTCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IFloorTTCMutSetEffect.id => id;
  public void visit(IFloorTTCMutSetEffectVisitor visitor) {
    visitor.visitFloorTTCMutSetCreateEffect(this);
  }
}

}
