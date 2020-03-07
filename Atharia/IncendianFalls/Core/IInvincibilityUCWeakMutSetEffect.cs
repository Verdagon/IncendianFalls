using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IInvincibilityUCWeakMutSetEffect {
  int id { get; }
  void visit(IInvincibilityUCWeakMutSetEffectVisitor visitor);
}

}
