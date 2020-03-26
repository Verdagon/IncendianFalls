using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICliffLandingTTCMutSetEffect : IEffect {
  int id { get; }
  void visitICliffLandingTTCMutSetEffect(ICliffLandingTTCMutSetEffectVisitor visitor);
}

}
