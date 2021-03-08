using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEvaporateImpulseStrongMutSetEffectVisitor {
  void visitEvaporateImpulseStrongMutSetCreateEffect(EvaporateImpulseStrongMutSetCreateEffect effect);
  void visitEvaporateImpulseStrongMutSetDeleteEffect(EvaporateImpulseStrongMutSetDeleteEffect effect);
  void visitEvaporateImpulseStrongMutSetAddEffect(EvaporateImpulseStrongMutSetAddEffect effect);
  void visitEvaporateImpulseStrongMutSetRemoveEffect(EvaporateImpulseStrongMutSetRemoveEffect effect);
}
         
}
