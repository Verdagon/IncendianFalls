using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITimeScriptDirectiveUCMutSetEffectVisitor {
  void visitTimeScriptDirectiveUCMutSetCreateEffect(TimeScriptDirectiveUCMutSetCreateEffect effect);
  void visitTimeScriptDirectiveUCMutSetDeleteEffect(TimeScriptDirectiveUCMutSetDeleteEffect effect);
  void visitTimeScriptDirectiveUCMutSetAddEffect(TimeScriptDirectiveUCMutSetAddEffect effect);
  void visitTimeScriptDirectiveUCMutSetRemoveEffect(TimeScriptDirectiveUCMutSetRemoveEffect effect);
}
         
}
