using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IInvincibilityUCWeakMutSetEffect : IEffect {
  int id { get; }
  void visitIInvincibilityUCWeakMutSetEffect(IInvincibilityUCWeakMutSetEffectVisitor visitor);
}

}
