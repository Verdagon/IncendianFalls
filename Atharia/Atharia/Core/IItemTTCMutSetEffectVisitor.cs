using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IItemTTCMutSetEffectVisitor {
  void visitItemTTCMutSetCreateEffect(ItemTTCMutSetCreateEffect effect);
  void visitItemTTCMutSetDeleteEffect(ItemTTCMutSetDeleteEffect effect);
  void visitItemTTCMutSetAddEffect(ItemTTCMutSetAddEffect effect);
  void visitItemTTCMutSetRemoveEffect(ItemTTCMutSetRemoveEffect effect);
}
         
}
