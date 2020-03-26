using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISorcerousUCMutSetEffect : IEffect {
  int id { get; }
  void visitISorcerousUCMutSetEffect(ISorcerousUCMutSetEffectVisitor visitor);
}

}
