using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseDefenseUCMutSetEffectVisitor {
  void visitBaseDefenseUCMutSetCreateEffect(BaseDefenseUCMutSetCreateEffect effect);
  void visitBaseDefenseUCMutSetDeleteEffect(BaseDefenseUCMutSetDeleteEffect effect);
  void visitBaseDefenseUCMutSetAddEffect(BaseDefenseUCMutSetAddEffect effect);
  void visitBaseDefenseUCMutSetRemoveEffect(BaseDefenseUCMutSetRemoveEffect effect);
}
         
}
