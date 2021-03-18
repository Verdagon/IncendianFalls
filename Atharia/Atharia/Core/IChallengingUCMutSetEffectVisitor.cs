using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IChallengingUCMutSetEffectVisitor {
  void visitChallengingUCMutSetCreateEffect(ChallengingUCMutSetCreateEffect effect);
  void visitChallengingUCMutSetDeleteEffect(ChallengingUCMutSetDeleteEffect effect);
  void visitChallengingUCMutSetAddEffect(ChallengingUCMutSetAddEffect effect);
  void visitChallengingUCMutSetRemoveEffect(ChallengingUCMutSetRemoveEffect effect);
}
         
}
