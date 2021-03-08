using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TutorialDefyCounterUCMutSetAddEffect : ITutorialDefyCounterUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public TutorialDefyCounterUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int ITutorialDefyCounterUCMutSetEffect.id => id;
  public void visitITutorialDefyCounterUCMutSetEffect(ITutorialDefyCounterUCMutSetEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
