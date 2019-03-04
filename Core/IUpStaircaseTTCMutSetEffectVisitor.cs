using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpStaircaseTTCMutSetEffectVisitor {
  void visitUpStaircaseTTCMutSetCreateEffect(UpStaircaseTTCMutSetCreateEffect effect);
  void visitUpStaircaseTTCMutSetDeleteEffect(UpStaircaseTTCMutSetDeleteEffect effect);
  void visitUpStaircaseTTCMutSetAddEffect(UpStaircaseTTCMutSetAddEffect effect);
  void visitUpStaircaseTTCMutSetRemoveEffect(UpStaircaseTTCMutSetRemoveEffect effect);
}
         
}
