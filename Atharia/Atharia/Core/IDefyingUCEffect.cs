using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDefyingUCEffect : IEffect {
  int id { get; }
  void visitIDefyingUCEffect(IDefyingUCEffectVisitor visitor);
}
       
}
