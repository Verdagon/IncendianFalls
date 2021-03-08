using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITutorialDefyCounterUCEffectVisitor {
  void visitTutorialDefyCounterUCCreateEffect(TutorialDefyCounterUCCreateEffect effect);
  void visitTutorialDefyCounterUCDeleteEffect(TutorialDefyCounterUCDeleteEffect effect);
  void visitTutorialDefyCounterUCSetNumDefiesRemainingEffect(TutorialDefyCounterUCSetNumDefiesRemainingEffect effect);
}

}
