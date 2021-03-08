using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITutorial1LevelControllerEffectVisitor {
  void visitTutorial1LevelControllerCreateEffect(Tutorial1LevelControllerCreateEffect effect);
  void visitTutorial1LevelControllerDeleteEffect(Tutorial1LevelControllerDeleteEffect effect);
}

}
