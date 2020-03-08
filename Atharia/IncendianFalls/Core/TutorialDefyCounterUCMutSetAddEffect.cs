using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TutorialDefyCounterUCMutSetAddEffect : ITutorialDefyCounterUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TutorialDefyCounterUCMutSetAddEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITutorialDefyCounterUCMutSetEffect.id => id;
  public void visit(ITutorialDefyCounterUCMutSetEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCMutSetAddEffect(this);
  }
}

}
