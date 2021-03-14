using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFlowerTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIFlowerTTCMutSetEffect(IFlowerTTCMutSetEffectVisitor visitor);
}

}
