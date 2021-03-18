using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ChallengingUCMutSetAddEffect : IChallengingUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ChallengingUCMutSetAddEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IChallengingUCMutSetEffect.id => id;
  public void visitIChallengingUCMutSetEffect(IChallengingUCMutSetEffectVisitor visitor) {
    visitor.visitChallengingUCMutSetAddEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitChallengingUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
