using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public struct Tutorial2LevelControllerCreateEffect : ITutorial2LevelControllerEffect {
  public readonly int id;
  public readonly Tutorial2LevelControllerIncarnation incarnation;
  public Tutorial2LevelControllerCreateEffect(int id, Tutorial2LevelControllerIncarnation incarnation) {
    this.id = id;
    this.incarnation = incarnation;
  }
  int ITutorial2LevelControllerEffect.id => id;
  public void visitITutorial2LevelControllerEffect(ITutorial2LevelControllerEffectVisitor visitor) {
    visitor.visitTutorial2LevelControllerCreateEffect(this);
  }
  public void visitIEffect(IEffectVisitor visitor) {
    visitor.visitTutorial2LevelControllerEffect(this);
  }
  public bool isSubtractive() { return false; }
}

}
