using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TutorialDefyCounterUCCreateEffect : ITutorialDefyCounterUCEffect {
  public readonly int id;
  public TutorialDefyCounterUCCreateEffect(int id) {
    this.id = id;
  }
  int ITutorialDefyCounterUCEffect.id => id;
  public void visit(ITutorialDefyCounterUCEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCCreateEffect(this);
  }
}

}
