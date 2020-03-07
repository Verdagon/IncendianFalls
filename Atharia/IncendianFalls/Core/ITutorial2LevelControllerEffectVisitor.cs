using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITutorial2LevelControllerEffectVisitor {
  void visitTutorial2LevelControllerCreateEffect(Tutorial2LevelControllerCreateEffect effect);
  void visitTutorial2LevelControllerDeleteEffect(Tutorial2LevelControllerDeleteEffect effect);
}

}
