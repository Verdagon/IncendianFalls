using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct GauntletLevelControllerCreateEffect : IGauntletLevelControllerEffect {
  public readonly int id;
  public readonly GauntletLevelControllerIncarnation incarnation;
  public GauntletLevelControllerCreateEffect(int id, GauntletLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IGauntletLevelControllerEffect.id => id;
  public void visitIGauntletLevelControllerEffect(IGauntletLevelControllerEffectVisitor visitor) {
    visitor.visitGauntletLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitGauntletLevelControllerEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
