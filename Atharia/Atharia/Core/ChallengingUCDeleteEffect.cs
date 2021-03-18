using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ChallengingUCDeleteEffect : IChallengingUCEffect {
  public readonly int id;
  public ChallengingUCDeleteEffect(int id) {
    this.id = id;
  }
  int IChallengingUCEffect.id => id;
  public void visitIChallengingUCEffect(IChallengingUCEffectVisitor visitor) {
    visitor.visitChallengingUCDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitChallengingUCEffect(this);
  }
  public bool isSubtractive() { return true; }
}

}
