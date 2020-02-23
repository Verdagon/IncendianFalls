using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IUpstairsTTCMutSetEffectVisitor {
  void visitUpstairsTTCMutSetCreateEffect(UpstairsTTCMutSetCreateEffect effect);
  void visitUpstairsTTCMutSetDeleteEffect(UpstairsTTCMutSetDeleteEffect effect);
  void visitUpstairsTTCMutSetAddEffect(UpstairsTTCMutSetAddEffect effect);
  void visitUpstairsTTCMutSetRemoveEffect(UpstairsTTCMutSetRemoveEffect effect);
}
         
}
