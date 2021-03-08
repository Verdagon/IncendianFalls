using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICounteringUCMutSetEffect : IEffect {
  int id { get; }
  void visitICounteringUCMutSetEffect(ICounteringUCMutSetEffectVisitor visitor);
}

}
