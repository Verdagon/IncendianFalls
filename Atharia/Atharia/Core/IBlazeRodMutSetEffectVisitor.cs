using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBlazeRodMutSetEffectVisitor {
  void visitBlazeRodMutSetCreateEffect(BlazeRodMutSetCreateEffect effect);
  void visitBlazeRodMutSetDeleteEffect(BlazeRodMutSetDeleteEffect effect);
  void visitBlazeRodMutSetAddEffect(BlazeRodMutSetAddEffect effect);
  void visitBlazeRodMutSetRemoveEffect(BlazeRodMutSetRemoveEffect effect);
}
         
}
