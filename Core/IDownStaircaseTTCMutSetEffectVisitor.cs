using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownStaircaseTTCMutSetEffectVisitor {
  void visitDownStaircaseTTCMutSetCreateEffect(DownStaircaseTTCMutSetCreateEffect effect);
  void visitDownStaircaseTTCMutSetDeleteEffect(DownStaircaseTTCMutSetDeleteEffect effect);
  void visitDownStaircaseTTCMutSetAddEffect(DownStaircaseTTCMutSetAddEffect effect);
  void visitDownStaircaseTTCMutSetRemoveEffect(DownStaircaseTTCMutSetRemoveEffect effect);
}
         
}
