using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct StaircaseTTCSetDestinationLevelPortalIndexEffect : IStaircaseTTCEffect {
  public readonly int id;
  public readonly int newValue;
  public StaircaseTTCSetDestinationLevelPortalIndexEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IStaircaseTTCEffect.id => id;

  public void visit(IStaircaseTTCEffectVisitor visitor) {
    visitor.visitStaircaseTTCSetDestinationLevelPortalIndexEffect(this);
  }
}

}
