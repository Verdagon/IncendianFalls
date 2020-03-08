using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TutorialDefyCounterUCMutSetCreateEffect : ITutorialDefyCounterUCMutSetEffect {
  public readonly int id;
  public TutorialDefyCounterUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int ITutorialDefyCounterUCMutSetEffect.id => id;
  public void visit(ITutorialDefyCounterUCMutSetEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCMutSetCreateEffect(this);
  }
}

}
