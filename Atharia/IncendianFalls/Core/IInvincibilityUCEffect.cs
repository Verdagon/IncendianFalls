using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IInvincibilityUCEffect {
  int id { get; }
  void visit(IInvincibilityUCEffectVisitor visitor);
}
       
}
