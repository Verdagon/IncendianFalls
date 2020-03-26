using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFireBombTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIFireBombTTCMutSetEffect(IFireBombTTCMutSetEffectVisitor visitor);
}

}
