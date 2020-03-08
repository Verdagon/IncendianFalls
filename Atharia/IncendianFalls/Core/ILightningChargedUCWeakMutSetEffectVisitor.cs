using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILightningChargedUCWeakMutSetEffectVisitor {
  void visitLightningChargedUCWeakMutSetCreateEffect(LightningChargedUCWeakMutSetCreateEffect effect);
  void visitLightningChargedUCWeakMutSetDeleteEffect(LightningChargedUCWeakMutSetDeleteEffect effect);
  void visitLightningChargedUCWeakMutSetAddEffect(LightningChargedUCWeakMutSetAddEffect effect);
  void visitLightningChargedUCWeakMutSetRemoveEffect(LightningChargedUCWeakMutSetRemoveEffect effect);
}
         
}
