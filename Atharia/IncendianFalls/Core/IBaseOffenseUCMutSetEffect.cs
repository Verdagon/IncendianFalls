using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseOffenseUCMutSetEffect : IEffect {
  int id { get; }
  void visitIBaseOffenseUCMutSetEffect(IBaseOffenseUCMutSetEffectVisitor visitor);
}

}
