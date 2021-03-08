using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMiredUCWeakMutSetEffect : IEffect {
  int id { get; }
  void visitIMiredUCWeakMutSetEffect(IMiredUCWeakMutSetEffectVisitor visitor);
}

}
