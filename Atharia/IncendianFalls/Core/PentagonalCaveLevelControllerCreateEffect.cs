using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct PentagonalCaveLevelControllerCreateEffect : IPentagonalCaveLevelControllerEffect {
  public readonly int id;
  public readonly PentagonalCaveLevelControllerIncarnation incarnation;
  public PentagonalCaveLevelControllerCreateEffect(int id, PentagonalCaveLevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int IPentagonalCaveLevelControllerEffect.id => id;
  public void visitIPentagonalCaveLevelControllerEffect(IPentagonalCaveLevelControllerEffectVisitor visitor) {
    visitor.visitPentagonalCaveLevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitPentagonalCaveLevelControllerEffect(this);
  }
}

}
