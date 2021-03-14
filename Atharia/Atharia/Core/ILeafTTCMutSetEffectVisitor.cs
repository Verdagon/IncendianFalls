using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ILeafTTCMutSetEffectVisitor {
  void visitLeafTTCMutSetCreateEffect(LeafTTCMutSetCreateEffect effect);
  void visitLeafTTCMutSetDeleteEffect(LeafTTCMutSetDeleteEffect effect);
  void visitLeafTTCMutSetAddEffect(LeafTTCMutSetAddEffect effect);
  void visitLeafTTCMutSetRemoveEffect(LeafTTCMutSetRemoveEffect effect);
}
         
}
