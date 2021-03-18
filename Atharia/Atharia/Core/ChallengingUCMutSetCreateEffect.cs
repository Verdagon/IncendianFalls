using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ChallengingUCMutSetCreateEffect : IChallengingUCMutSetEffect {
  public readonly int id;
  public ChallengingUCMutSetCreateEffect(int id) {
    this.id = id;
  }
  int IChallengingUCMutSetEffect.id => id;
  public void visitIChallengingUCMutSetEffect(IChallengingUCMutSetEffectVisitor visitor) {
    visitor.visitChallengingUCMutSetCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitChallengingUCMutSetEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
