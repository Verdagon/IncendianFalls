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
  public void visit(ITutorial2LevelControllerEffectVisitor visitor) {
    visitor.visitTutorial2LevelControllerDeleteEffect(this);
  }
}

}
