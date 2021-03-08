using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IInvincibilityUCMutSetEffect : IEffect {
  int id { get; }
  void visitIInvincibilityUCMutSetEffect(IInvincibilityUCMutSetEffectVisitor visitor);
}

}
