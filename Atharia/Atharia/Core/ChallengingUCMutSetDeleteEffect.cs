using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public struct ChallengingUCMutSetDeleteEffect : IChallengingUCMutSetEffect {
  public readonly int id;
  public ChallengingUCMutSetDeleteEffect(int id) {
    this.id = id;
  }
  int IChallengingUCMutSetEffect.id => id;
  public void visitIChallengingUCMutSetEffect(IChallengingUCMutSetEffectVisitor visitor) {
    visitor.visitChallengingUCMutSetDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitChallengingUCMutSetEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
