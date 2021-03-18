using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ChallengingUCIncarnation : IChallengingUCEffectVisitor {
  public ChallengingUCIncarnation(
) {
  }
  public ChallengingUCIncarnation Copy() {
    return new ChallengingUCIncarnation(
    );
  }

  public void visitChallengingUCCreateEffect(ChallengingUCCreateEffect e) {}
  public void visitChallengingUCDeleteEffect(ChallengingUCDeleteEffect e) {}

  public void ApplyEffect(IChallengingUCEffect effect) { effect.visitIChallengingUCEffect(this); }
}

}
