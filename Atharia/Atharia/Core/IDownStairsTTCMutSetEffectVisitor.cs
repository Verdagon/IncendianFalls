using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDownStairsTTCMutSetEffectVisitor {
  void visitDownStairsTTCMutSetCreateEffect(DownStairsTTCMutSetCreateEffect effect);
  void visitDownStairsTTCMutSetDeleteEffect(DownStairsTTCMutSetDeleteEffect effect);
  void visitDownStairsTTCMutSetAddEffect(DownStairsTTCMutSetAddEffect effect);
  void visitDownStairsTTCMutSetRemoveEffect(DownStairsTTCMutSetRemoveEffect effect);
}
         
}
