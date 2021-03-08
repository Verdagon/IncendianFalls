using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICounteringUCWeakMutSetEffect : IEffect {
  int id { get; }
  void visitICounteringUCWeakMutSetEffect(ICounteringUCWeakMutSetEffectVisitor visitor);
}

}
