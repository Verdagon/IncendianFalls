using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IKillDirectiveEffect : IEffect {
  int id { get; }
  void visitIKillDirectiveEffect(IKillDirectiveEffectVisitor visitor);
}
       
}
