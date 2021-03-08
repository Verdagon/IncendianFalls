using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICounteringUCWeakMutSetEffectVisitor {
  void visitCounteringUCWeakMutSetCreateEffect(CounteringUCWeakMutSetCreateEffect effect);
  void visitCounteringUCWeakMutSetDeleteEffect(CounteringUCWeakMutSetDeleteEffect effect);
  void visitCounteringUCWeakMutSetAddEffect(CounteringUCWeakMutSetAddEffect effect);
  void visitCounteringUCWeakMutSetRemoveEffect(CounteringUCWeakMutSetRemoveEffect effect);
}
         
}
