using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct TutorialLevelControllerCreateEffect : ITutorialLevelControllerEffect {
  public readonly int id;
  public TutorialLevelControllerCreateEffect(int id) {
    this.id = id;
  }
  int ITutorialLevelControllerEffect.id => id;
  public void visit(ITutorialLevelControllerEffectVisitor visitor) {
    visitor.visitTutorialLevelControllerCreateEffect(this);
  }
}

}
