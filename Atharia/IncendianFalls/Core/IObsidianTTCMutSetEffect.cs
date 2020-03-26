using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IObsidianTTCMutSetEffect : IEffect {
  int id { get; }
  void visitIObsidianTTCMutSetEffect(IObsidianTTCMutSetEffectVisitor visitor);
}

}
