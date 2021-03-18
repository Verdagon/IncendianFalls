using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct ChallengingUCCreateEffect : IChallengingUCEffect {
  public readonly int id;
  public readonly ChallengingUCIncarnation incarnation;
  public ChallengingUCCreateEffect(int id, ChallengingUCIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IChallengingUCEffect.id => id;
  public void visitIChallengingUCEffect(IChallengingUCEffectVisitor visitor) {
    visitor.visitChallengingUCCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitChallengingUCEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
