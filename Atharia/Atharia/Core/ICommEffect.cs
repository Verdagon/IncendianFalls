using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICommEffect : IEffect {
  int id { get; }
  void visitICommEffect(ICommEffectVisitor visitor);
}
       
}
