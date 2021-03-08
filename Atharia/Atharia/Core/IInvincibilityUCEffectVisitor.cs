using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IInvincibilityUCEffectVisitor {
  void visitInvincibilityUCCreateEffect(InvincibilityUCCreateEffect effect);
  void visitInvincibilityUCDeleteEffect(InvincibilityUCDeleteEffect effect);
}

}
