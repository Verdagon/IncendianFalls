using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefyingUCWeakMutSetEffectVisitor {
  void visitDefyingUCWeakMutSetCreateEffect(DefyingUCWeakMutSetCreateEffect effect);
  void visitDefyingUCWeakMutSetDeleteEffect(DefyingUCWeakMutSetDeleteEffect effect);
  void visitDefyingUCWeakMutSetAddEffect(DefyingUCWeakMutSetAddEffect effect);
  void visitDefyingUCWeakMutSetRemoveEffect(DefyingUCWeakMutSetRemoveEffect effect);
}
         
}
