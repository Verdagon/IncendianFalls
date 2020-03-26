using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMiredUCMutSetEffect : IEffect {
  int id { get; }
  void visitIMiredUCMutSetEffect(IMiredUCMutSetEffectVisitor visitor);
}

}
