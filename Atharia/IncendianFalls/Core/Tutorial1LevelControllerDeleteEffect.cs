using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct Tutorial1LevelControllerDeleteEffect : ITutorial1LevelControllerEffect {
  public readonly int id;
  public Tutorial1LevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int ITutorial1LevelControllerEffect.id => id;
  public void visit(ITutorial1LevelControllerEffectVisitor visitor) {
    visitor.visitTutorial1LevelControllerDeleteEffect(this);
  }
}

}
