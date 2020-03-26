using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDefyingUCMutSetEffect : IEffect {
  int id { get; }
  void visitIDefyingUCMutSetEffect(IDefyingUCMutSetEffectVisitor visitor);
}

}
