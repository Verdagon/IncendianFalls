using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownstairsTTCMutSetEffectVisitor {
  void visitDownstairsTTCMutSetCreateEffect(DownstairsTTCMutSetCreateEffect effect);
  void visitDownstairsTTCMutSetDeleteEffect(DownstairsTTCMutSetDeleteEffect effect);
  void visitDownstairsTTCMutSetAddEffect(DownstairsTTCMutSetAddEffect effect);
  void visitDownstairsTTCMutSetRemoveEffect(DownstairsTTCMutSetRemoveEffect effect);
}
         
}
