using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IOnFireUCWeakMutSetEffect : IEffect {
  int id { get; }
  void visitIOnFireUCWeakMutSetEffect(IOnFireUCWeakMutSetEffectVisitor visitor);
}

}
