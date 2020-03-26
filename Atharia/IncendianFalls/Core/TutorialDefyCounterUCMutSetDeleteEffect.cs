using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct TutorialDefyCounterUCMutSetDeleteEffect : ITutorialDefyCounterUCMutSetEffect {
  public readonly int id;
  public TutorialDefyCounterUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int ITutorialDefyCounterUCMutSetEffect.id => id;
  public void visitITutorialDefyCounterUCMutSetEffect(ITutorialDefyCounterUCMutSetEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCMutSetEffect(this);
  }
}

}
