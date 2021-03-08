using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefyingUCMutSetEffectVisitor {
  void visitDefyingUCMutSetCreateEffect(DefyingUCMutSetCreateEffect effect);
  void visitDefyingUCMutSetDeleteEffect(DefyingUCMutSetDeleteEffect effect);
  void visitDefyingUCMutSetAddEffect(DefyingUCMutSetAddEffect effect);
  void visitDefyingUCMutSetRemoveEffect(DefyingUCMutSetRemoveEffect effect);
}
         
}
