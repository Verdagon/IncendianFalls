using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISlowRodMutSetEffect : IEffect {
  int id { get; }
  void visitISlowRodMutSetEffect(ISlowRodMutSetEffectVisitor visitor);
}

}
