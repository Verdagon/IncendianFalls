using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class Tutorial1LevelControllerIncarnation : ITutorial1LevelControllerEffectVisitor {
  public readonly int level;
  public Tutorial1LevelControllerIncarnation(
      int level) {
    this.level = level;
  }
  public Tutorial1LevelControllerIncarnation Copy() {
    return new Tutorial1LevelControllerIncarnation(
level    );
  }

  public void visitTutorial1LevelControllerCreateEffect(Tutorial1LevelControllerCreateEffect e) {}
  public void visitTutorial1LevelControllerDeleteEffect(Tutorial1LevelControllerDeleteEffect e) {}

  public void ApplyEffect(ITutorial1LevelControllerEffect effect) { effect.visitITutorial1LevelControllerEffect(this); }
}

}
