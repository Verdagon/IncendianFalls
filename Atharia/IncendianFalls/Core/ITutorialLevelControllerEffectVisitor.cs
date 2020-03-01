using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITutorialLevelControllerEffectVisitor {
  void visitTutorialLevelControllerCreateEffect(TutorialLevelControllerCreateEffect effect);
  void visitTutorialLevelControllerDeleteEffect(TutorialLevelControllerDeleteEffect effect);
}

}
