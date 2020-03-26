using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TutorialDefyCounterUCDeleteEffect : ITutorialDefyCounterUCEffect {
  public readonly int id;
  public TutorialDefyCounterUCDeleteEffect(int id) {
    this.id = id;
  }
  int ITutorialDefyCounterUCEffect.id => id;
  public void visitITutorialDefyCounterUCEffect(ITutorialDefyCounterUCEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCEffect(this);
  }
}

}
