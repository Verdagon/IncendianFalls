using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseOffenseUCMutSetEffectVisitor {
  void visitBaseOffenseUCMutSetCreateEffect(BaseOffenseUCMutSetCreateEffect effect);
  void visitBaseOffenseUCMutSetDeleteEffect(BaseOffenseUCMutSetDeleteEffect effect);
  void visitBaseOffenseUCMutSetAddEffect(BaseOffenseUCMutSetAddEffect effect);
  void visitBaseOffenseUCMutSetRemoveEffect(BaseOffenseUCMutSetRemoveEffect effect);
}
         
}
