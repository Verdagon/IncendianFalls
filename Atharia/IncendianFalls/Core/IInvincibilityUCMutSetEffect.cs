using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IInvincibilityUCMutSetEffect {
  int id { get; }
  void visit(IInvincibilityUCMutSetEffectVisitor visitor);
}

}
