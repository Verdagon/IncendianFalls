using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICliffTTCMutSetEffect : IEffect {
  int id { get; }
  void visitICliffTTCMutSetEffect(ICliffTTCMutSetEffectVisitor visitor);
}

}
