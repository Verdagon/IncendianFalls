using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IEvolvifyImpulseStrongMutSetEffectVisitor {
  void visitEvolvifyImpulseStrongMutSetCreateEffect(EvolvifyImpulseStrongMutSetCreateEffect effect);
  void visitEvolvifyImpulseStrongMutSetDeleteEffect(EvolvifyImpulseStrongMutSetDeleteEffect effect);
  void visitEvolvifyImpulseStrongMutSetAddEffect(EvolvifyImpulseStrongMutSetAddEffect effect);
  void visitEvolvifyImpulseStrongMutSetRemoveEffect(EvolvifyImpulseStrongMutSetRemoveEffect effect);
}
         
}
