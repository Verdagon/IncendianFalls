using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWaterTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIWaterTTCMutSetEffect(IWaterTTCMutSetEffectVisitor visitor);
}

}
