using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefyImpulseStrongMutSetEffectVisitor {
  void visitDefyImpulseStrongMutSetCreateEffect(DefyImpulseStrongMutSetCreateEffect effect);
  void visitDefyImpulseStrongMutSetDeleteEffect(DefyImpulseStrongMutSetDeleteEffect effect);
  void visitDefyImpulseStrongMutSetAddEffect(DefyImpulseStrongMutSetAddEffect effect);
  void visitDefyImpulseStrongMutSetRemoveEffect(DefyImpulseStrongMutSetRemoveEffect effect);
}
         
}
