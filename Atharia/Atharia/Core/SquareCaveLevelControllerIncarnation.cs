using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SquareCaveLevelControllerIncarnation : ISquareCaveLevelControllerEffectVisitor {
  public readonly int level;
  public readonly int depth;
  public SquareCaveLevelControllerIncarnation(
      int level,
      int depth) {
    this.level = level;
    this.depth = depth;
  }
  public SquareCaveLevelControllerIncarnation Copy() {
    return new SquareCaveLevelControllerIncarnation(
level,
depth    );
  }

  public void visitSquareCaveLevelControllerCreateEffect(SquareCaveLevelControllerCreateEffect e) {}
  public void visitSquareCaveLevelControllerDeleteEffect(SquareCaveLevelControllerDeleteEffect e) {}


  public void ApplyEffect(ISquareCaveLevelControllerEffect effect) { effect.visitISquareCaveLevelControllerEffect(this); }
}

}
