using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWallTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIWallTTCMutSetEffect(IWallTTCMutSetEffectVisitor visitor);
}

}
