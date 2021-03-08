using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class PentagonalCaveLevelControllerIncarnation : IPentagonalCaveLevelControllerEffectVisitor {
  public readonly int level;
  public readonly int depth;
  public PentagonalCaveLevelControllerIncarnation(
      int level,
      int depth) {
    this.level = level;
    this.depth = depth;
  }
  public PentagonalCaveLevelControllerIncarnation Copy() {
    return new PentagonalCaveLevelControllerIncarnation(
level,
depth    );
  }

  public void visitPentagonalCaveLevelControllerCreateEffect(PentagonalCaveLevelControllerCreateEffect e) {}
  public void visitPentagonalCaveLevelControllerDeleteEffect(PentagonalCaveLevelControllerDeleteEffect e) {}


  public void ApplyEffect(IPentagonalCaveLevelControllerEffect effect) { effect.visitIPentagonalCaveLevelControllerEffect(this); }
}

}
