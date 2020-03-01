using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TutorialLevelControllerDeleteEffect : ITutorialLevelControllerEffect {
  public readonly int id;
  public TutorialLevelControllerDeleteEffect(int id) {
    this.id = id;
  }
  int ITutorialLevelControllerEffect.id => id;
  public void visit(ITutorialLevelControllerEffectVisitor visitor) {
    visitor.visitTutorialLevelControllerDeleteEffect(this);
  }
}

}
