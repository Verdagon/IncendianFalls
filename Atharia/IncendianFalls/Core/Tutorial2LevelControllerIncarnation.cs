using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class Tutorial2LevelControllerIncarnation : ITutorial2LevelControllerEffectVisitor {
  public readonly int level;
  public Tutorial2LevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public Tutorial2LevelControllerIncarnation Copy() {
    return new Tutorial2LevelControllerIncarnation(
level    );
  }

  public void visitTutorial2LevelControllerCreateEffect(Tutorial2LevelControllerCreateEffect e) {}
  public void visitTutorial2LevelControllerDeleteEffect(Tutorial2LevelControllerDeleteEffect e) {}

  public void ApplyEffect(ITutorial2LevelControllerEffect effect) { effect.visitITutorial2LevelControllerEffect(this); }
}

}
