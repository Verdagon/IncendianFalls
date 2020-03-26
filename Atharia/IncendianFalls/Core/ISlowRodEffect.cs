using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ISlowRodEffect : IEffect {
  int id { get; }
  void visitISlowRodEffect(ISlowRodEffectVisitor visitor);
}
       
}
