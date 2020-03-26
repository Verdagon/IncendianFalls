using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CaveLevelControllerIncarnation : ICaveLevelControllerEffectVisitor {
  public readonly int level;
  public readonly int depth;
  public CaveLevelControllerIncarnation(
      int level,
      int depth) {
    this.level = level;
    this.depth = depth;
  }
  public CaveLevelControllerIncarnation Copy() {
    return new CaveLevelControllerIncarnation(
level,
depth    );
  }

  public void visitCaveLevelControllerCreateEffect(CaveLevelControllerCreateEffect e) {}
  public void visitCaveLevelControllerDeleteEffect(CaveLevelControllerDeleteEffect e) {}


  public void ApplyEffect(ICaveLevelControllerEffect effect) { effect.visitICaveLevelControllerEffect(this); }
}

}
