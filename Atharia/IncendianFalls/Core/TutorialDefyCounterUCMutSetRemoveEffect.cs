using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TutorialDefyCounterUCMutSetRemoveEffect : ITutorialDefyCounterUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TutorialDefyCounterUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITutorialDefyCounterUCMutSetEffect.id => id;
  public void visitITutorialDefyCounterUCMutSetEffect(ITutorialDefyCounterUCMutSetEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCMutSetEffect(this);
  }
}

}
