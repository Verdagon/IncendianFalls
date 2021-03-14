using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IOnFireUCMutSetEffectVisitor {
  void visitOnFireUCMutSetCreateEffect(OnFireUCMutSetCreateEffect effect);
  void visitOnFireUCMutSetDeleteEffect(OnFireUCMutSetDeleteEffect effect);
  void visitOnFireUCMutSetAddEffect(OnFireUCMutSetAddEffect effect);
  void visitOnFireUCMutSetRemoveEffect(OnFireUCMutSetRemoveEffect effect);
}
         
}
