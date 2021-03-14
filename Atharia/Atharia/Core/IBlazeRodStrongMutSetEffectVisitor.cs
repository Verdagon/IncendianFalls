using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBlazeRodStrongMutSetEffectVisitor {
  void visitBlazeRodStrongMutSetCreateEffect(BlazeRodStrongMutSetCreateEffect effect);
  void visitBlazeRodStrongMutSetDeleteEffect(BlazeRodStrongMutSetDeleteEffect effect);
  void visitBlazeRodStrongMutSetAddEffect(BlazeRodStrongMutSetAddEffect effect);
  void visitBlazeRodStrongMutSetRemoveEffect(BlazeRodStrongMutSetRemoveEffect effect);
}
         
}
