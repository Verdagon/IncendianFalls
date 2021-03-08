using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct PreGauntletLevelControllerCreateEffect : IPreGauntletLevelControllerEffect {
  public readonly int id;
  public readonly PreGauntletLevelControllerIncarnation incarnation;
  public PreGauntletLevelControllerCreateEffect(int id, PreGauntletLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IPreGauntletLevelControllerEffect.id => id;
  public void visitIPreGauntletLevelControllerEffect(IPreGauntletLevelControllerEffectVisitor visitor) {
    visitor.visitPreGauntletLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitPreGauntletLevelControllerEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
