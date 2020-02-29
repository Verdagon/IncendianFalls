using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDirtTTCMutSetEffectVisitor {
  void visitDirtTTCMutSetCreateEffect(DirtTTCMutSetCreateEffect effect);
  void visitDirtTTCMutSetDeleteEffect(DirtTTCMutSetDeleteEffect effect);
  void visitDirtTTCMutSetAddEffect(DirtTTCMutSetAddEffect effect);
  void visitDirtTTCMutSetRemoveEffect(DirtTTCMutSetRemoveEffect effect);
}
         
}
