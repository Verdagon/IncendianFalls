using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct StaircaseTTCSetDestinationLevelEffect : IStaircaseTTCEffect {
  public readonly int id;
  public readonly Level newValue;
  public StaircaseTTCSetDestinationLevelEffect(
      int id,
      Level newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int IStaircaseTTCEffect.id => id;

  public void visit(IStaircaseTTCEffectVisitor visitor) {
    visitor.visitStaircaseTTCSetDestinationLevelEffect(this);
  }
}

}
