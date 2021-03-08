using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefyingUCWeakMutSetEffect : IEffect {
  int id { get; }
  void visitIDefyingUCWeakMutSetEffect(IDefyingUCWeakMutSetEffectVisitor visitor);
}

}
