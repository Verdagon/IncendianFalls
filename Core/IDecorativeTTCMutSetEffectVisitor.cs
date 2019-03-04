using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDecorativeTTCMutSetEffectVisitor {
  void visitDecorativeTTCMutSetCreateEffect(DecorativeTTCMutSetCreateEffect effect);
  void visitDecorativeTTCMutSetDeleteEffect(DecorativeTTCMutSetDeleteEffect effect);
  void visitDecorativeTTCMutSetAddEffect(DecorativeTTCMutSetAddEffect effect);
  void visitDecorativeTTCMutSetRemoveEffect(DecorativeTTCMutSetRemoveEffect effect);
}
         
}
