using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct Tutorial1LevelControllerCreateEffect : ITutorial1LevelControllerEffect {
  public readonly int id;
  public readonly Tutorial1LevelControllerIncarnation incarnation;
  public Tutorial1LevelControllerCreateEffect(int id, Tutorial1LevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ITutorial1LevelControllerEffect.id => id;
  public void visitITutorial1LevelControllerEffect(ITutorial1LevelControllerEffectVisitor visitor) {
    visitor.visitTutorial1LevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTutorial1LevelControllerEffect(this);
  }
}

}
