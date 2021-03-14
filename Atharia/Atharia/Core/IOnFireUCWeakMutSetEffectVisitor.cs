using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IOnFireUCWeakMutSetEffectVisitor {
  void visitOnFireUCWeakMutSetCreateEffect(OnFireUCWeakMutSetCreateEffect effect);
  void visitOnFireUCWeakMutSetDeleteEffect(OnFireUCWeakMutSetDeleteEffect effect);
  void visitOnFireUCWeakMutSetAddEffect(OnFireUCWeakMutSetAddEffect effect);
  void visitOnFireUCWeakMutSetRemoveEffect(OnFireUCWeakMutSetRemoveEffect effect);
}
         
}
