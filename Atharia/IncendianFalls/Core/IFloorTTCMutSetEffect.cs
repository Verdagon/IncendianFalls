using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IFloorTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIFloorTTCMutSetEffect(IFloorTTCMutSetEffectVisitor visitor);
}

}
