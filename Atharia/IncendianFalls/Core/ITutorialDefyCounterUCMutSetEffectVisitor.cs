using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITutorialDefyCounterUCMutSetEffectVisitor {
  void visitTutorialDefyCounterUCMutSetCreateEffect(TutorialDefyCounterUCMutSetCreateEffect effect);
  void visitTutorialDefyCounterUCMutSetDeleteEffect(TutorialDefyCounterUCMutSetDeleteEffect effect);
  void visitTutorialDefyCounterUCMutSetAddEffect(TutorialDefyCounterUCMutSetAddEffect effect);
  void visitTutorialDefyCounterUCMutSetRemoveEffect(TutorialDefyCounterUCMutSetRemoveEffect effect);
}
         
}
