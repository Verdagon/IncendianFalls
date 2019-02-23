using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKillDirectiveUCMutSetEffectVisitor {
  void visitKillDirectiveUCMutSetCreateEffect(KillDirectiveUCMutSetCreateEffect effect);
  void visitKillDirectiveUCMutSetDeleteEffect(KillDirectiveUCMutSetDeleteEffect effect);
  void visitKillDirectiveUCMutSetAddEffect(KillDirectiveUCMutSetAddEffect effect);
  void visitKillDirectiveUCMutSetRemoveEffect(KillDirectiveUCMutSetRemoveEffect effect);
}
         
}
