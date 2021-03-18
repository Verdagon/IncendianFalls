using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ChallengingUCMutSetRemoveEffect : IChallengingUCMutSetEffect {
  public readonly int id;
  public readonly int element;
  public ChallengingUCMutSetRemoveEffect(int id, int element) {
    this.id = id;
    this.element = element;
  }
  int IChallengingUCMutSetEffect.id => id;
  public void visitIChallengingUCMutSetEffect(IChallengingUCMutSetEffectVisitor visitor) {
    visitor.visitChallengingUCMutSetRemoveEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitChallengingUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
