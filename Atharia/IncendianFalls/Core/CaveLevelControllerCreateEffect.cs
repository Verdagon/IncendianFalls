using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct CaveLevelControllerCreateEffect : ICaveLevelControllerEffect {
  public readonly int id;
  public readonly CaveLevelControllerIncarnation incarnation;
  public CaveLevelControllerCreateEffect(int id, CaveLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ICaveLevelControllerEffect.id => id;
  public void visitICaveLevelControllerEffect(ICaveLevelControllerEffectVisitor visitor) {
    visitor.visitCaveLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitCaveLevelControllerEffect(this);
  }
}

}
