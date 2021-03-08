using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IInvincibilityUCWeakMutSetEffectVisitor {
  void visitInvincibilityUCWeakMutSetCreateEffect(InvincibilityUCWeakMutSetCreateEffect effect);
  void visitInvincibilityUCWeakMutSetDeleteEffect(InvincibilityUCWeakMutSetDeleteEffect effect);
  void visitInvincibilityUCWeakMutSetAddEffect(InvincibilityUCWeakMutSetAddEffect effect);
  void visitInvincibilityUCWeakMutSetRemoveEffect(InvincibilityUCWeakMutSetRemoveEffect effect);
}
         
}
