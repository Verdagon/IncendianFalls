using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDoomedUCMutSetEffect : IEffect {
  int id { get; }
  void visitIDoomedUCMutSetEffect(IDoomedUCMutSetEffectVisitor visitor);
}

}
