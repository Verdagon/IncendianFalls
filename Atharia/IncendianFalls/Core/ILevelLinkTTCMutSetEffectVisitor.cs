using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILevelLinkTTCMutSetEffectVisitor {
  void visitLevelLinkTTCMutSetCreateEffect(LevelLinkTTCMutSetCreateEffect effect);
  void visitLevelLinkTTCMutSetDeleteEffect(LevelLinkTTCMutSetDeleteEffect effect);
  void visitLevelLinkTTCMutSetAddEffect(LevelLinkTTCMutSetAddEffect effect);
  void visitLevelLinkTTCMutSetRemoveEffect(LevelLinkTTCMutSetRemoveEffect effect);
}
         
}
