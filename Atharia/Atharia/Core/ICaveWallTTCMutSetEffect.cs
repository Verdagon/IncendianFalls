using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICaveWallTTCMutSetEffect : IEffect {
  int id { get; }
  void visitICaveWallTTCMutSetEffect(ICaveWallTTCMutSetEffectVisitor visitor);
}

}
