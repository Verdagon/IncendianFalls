using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TutorialDefyCounterUCSetNumDefiesRemainingEffect : ITutorialDefyCounterUCEffect {
  public readonly int id;
  public readonly int newValue;
  public TutorialDefyCounterUCSetNumDefiesRemainingEffect(
      int id,
      int newValue) {
    this.id = id;
    this.newValue = newValue;
  }
  int ITutorialDefyCounterUCEffect.id => id;

  public void visit(ITutorialDefyCounterUCEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCSetNumDefiesRemainingEffect(this);
  }
}

}