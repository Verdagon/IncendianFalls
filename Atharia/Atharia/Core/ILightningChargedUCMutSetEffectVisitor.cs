using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILightningChargedUCMutSetEffectVisitor {
  void visitLightningChargedUCMutSetCreateEffect(LightningChargedUCMutSetCreateEffect effect);
  void visitLightningChargedUCMutSetDeleteEffect(LightningChargedUCMutSetDeleteEffect effect);
  void visitLightningChargedUCMutSetAddEffect(LightningChargedUCMutSetAddEffect effect);
  void visitLightningChargedUCMutSetRemoveEffect(LightningChargedUCMutSetRemoveEffect effect);
}
         
}
