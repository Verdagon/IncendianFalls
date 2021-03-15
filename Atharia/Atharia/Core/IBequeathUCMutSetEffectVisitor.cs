using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBequeathUCMutSetEffectVisitor {
  void visitBequeathUCMutSetCreateEffect(BequeathUCMutSetCreateEffect effect);
  void visitBequeathUCMutSetDeleteEffect(BequeathUCMutSetDeleteEffect effect);
  void visitBequeathUCMutSetAddEffect(BequeathUCMutSetAddEffect effect);
  void visitBequeathUCMutSetRemoveEffect(BequeathUCMutSetRemoveEffect effect);
}
         
}
