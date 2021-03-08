using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDoomedUCWeakMutSetEffect : IEffect {
  int id { get; }
  void visitIDoomedUCWeakMutSetEffect(IDoomedUCWeakMutSetEffectVisitor visitor);
}

}
