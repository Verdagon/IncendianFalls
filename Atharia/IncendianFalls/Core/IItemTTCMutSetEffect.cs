using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IItemTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIItemTTCMutSetEffect(IItemTTCMutSetEffectVisitor visitor);
}

}
