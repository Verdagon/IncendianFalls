using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TutorialDefyCounterUCCreateEffect : ITutorialDefyCounterUCEffect {
  public readonly int id;
  public readonly TutorialDefyCounterUCIncarnation incarnation;
  public TutorialDefyCounterUCCreateEffect(int id, TutorialDefyCounterUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ITutorialDefyCounterUCEffect.id => id;
  public void visitITutorialDefyCounterUCEffect(ITutorialDefyCounterUCEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTutorialDefyCounterUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
