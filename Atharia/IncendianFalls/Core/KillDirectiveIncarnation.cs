using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class KillDirectiveIncarnation : IKillDirectiveEffectVisitor {
  public readonly int targetUnit;
  public readonly int pathToLastSeenLocation;
  public KillDirectiveIncarnation(
      int targetUnit,
      int pathToLastSeenLocation) {
    this.targetUnit = targetUnit;
    this.pathToLastSeenLocation = pathToLastSeenLocation;
  }
  public KillDirectiveIncarnation Copy() {
    return new KillDirectiveIncarnation(
targetUnit,
pathToLastSeenLocation    );
  }

  public void visitKillDirectiveCreateEffect(KillDirectiveCreateEffect e) {}
  public void visitKillDirectiveDeleteEffect(KillDirectiveDeleteEffect e) {}


  public void ApplyEffect(IKillDirectiveEffect effect) { effect.visitIKillDirectiveEffect(this); }
}

}
