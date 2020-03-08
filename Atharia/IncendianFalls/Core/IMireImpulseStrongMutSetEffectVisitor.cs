using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMireImpulseStrongMutSetEffectVisitor {
  void visitMireImpulseStrongMutSetCreateEffect(MireImpulseStrongMutSetCreateEffect effect);
  void visitMireImpulseStrongMutSetDeleteEffect(MireImpulseStrongMutSetDeleteEffect effect);
  void visitMireImpulseStrongMutSetAddEffect(MireImpulseStrongMutSetAddEffect effect);
  void visitMireImpulseStrongMutSetRemoveEffect(MireImpulseStrongMutSetRemoveEffect effect);
}
         
}
