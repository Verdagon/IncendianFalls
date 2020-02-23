using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICounteringUCMutSetEffectVisitor {
  void visitCounteringUCMutSetCreateEffect(CounteringUCMutSetCreateEffect effect);
  void visitCounteringUCMutSetDeleteEffect(CounteringUCMutSetDeleteEffect effect);
  void visitCounteringUCMutSetAddEffect(CounteringUCMutSetAddEffect effect);
  void visitCounteringUCMutSetRemoveEffect(CounteringUCMutSetRemoveEffect effect);
}
         
}
