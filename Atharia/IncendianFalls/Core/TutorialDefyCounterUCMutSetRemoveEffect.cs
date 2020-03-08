using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TutorialDefyCounterUCMutSetRemoveEffect : ITutorialDefyCounterUCMutSetEffect {
  public readonly int id;
  public readonly int elementId;
  public TutorialDefyCounterUCMutSetRemoveEffect(int id, int elementId) {
    this.id = id;
    this.elementId = elementId;
  }
  int ITutorialDefyCounterUCMutSetEffect.id => id;
  public void visit(ITutorialDefyCounterUCMutSetEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCMutSetRemoveEffect(this);
  }
}

}
