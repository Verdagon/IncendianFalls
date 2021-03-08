using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class NestLevelControllerIncarnation : INestLevelControllerEffectVisitor {
  public readonly int level;
  public NestLevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public NestLevelControllerIncarnation Copy() {
    return new NestLevelControllerIncarnation(
level    );
  }

  public void visitNestLevelControllerCreateEffect(NestLevelControllerCreateEffect e) {}
  public void visitNestLevelControllerDeleteEffect(NestLevelControllerDeleteEffect e) {}

  public void ApplyEffect(INestLevelControllerEffect effect) { effect.visitINestLevelControllerEffect(this); }
}

}
