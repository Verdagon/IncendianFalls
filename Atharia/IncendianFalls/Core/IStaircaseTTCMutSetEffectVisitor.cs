using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IStaircaseTTCMutSetEffectVisitor {
  void visitStaircaseTTCMutSetCreateEffect(StaircaseTTCMutSetCreateEffect effect);
  void visitStaircaseTTCMutSetDeleteEffect(StaircaseTTCMutSetDeleteEffect effect);
  void visitStaircaseTTCMutSetAddEffect(StaircaseTTCMutSetAddEffect effect);
  void visitStaircaseTTCMutSetRemoveEffect(StaircaseTTCMutSetRemoveEffect effect);
}
         
}
