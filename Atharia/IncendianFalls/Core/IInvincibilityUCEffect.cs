using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IInvincibilityUCEffect : IEffect {
  int id { get; }
  void visitIInvincibilityUCEffect(IInvincibilityUCEffectVisitor visitor);
}
       
}
