using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IInvincibilityUCMutSetEffectVisitor {
  void visitInvincibilityUCMutSetCreateEffect(InvincibilityUCMutSetCreateEffect effect);
  void visitInvincibilityUCMutSetDeleteEffect(InvincibilityUCMutSetDeleteEffect effect);
  void visitInvincibilityUCMutSetAddEffect(InvincibilityUCMutSetAddEffect effect);
  void visitInvincibilityUCMutSetRemoveEffect(InvincibilityUCMutSetRemoveEffect effect);
}
         
}
