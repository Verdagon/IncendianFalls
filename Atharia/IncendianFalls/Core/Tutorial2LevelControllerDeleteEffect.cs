using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct Tutorial2LevelControllerDeleteEffect : ITutorial2LevelControllerEffect {
  public readonly int id;
  public Tutorial2LevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int ITutorial2LevelControllerEffect.id => id;
  public void visitITutorial2LevelControllerEffect(ITutorial2LevelControllerEffectVisitor visitor) {
    visitor.visitTutorial2LevelControllerDeleteEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTutorial2LevelControllerEffect(this);
  }
}

}
