using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISorcerousUCMutSetEffectVisitor {
  void visitSorcerousUCMutSetCreateEffect(SorcerousUCMutSetCreateEffect effect);
  void visitSorcerousUCMutSetDeleteEffect(SorcerousUCMutSetDeleteEffect effect);
  void visitSorcerousUCMutSetAddEffect(SorcerousUCMutSetAddEffect effect);
  void visitSorcerousUCMutSetRemoveEffect(SorcerousUCMutSetRemoveEffect effect);
}
         
}
