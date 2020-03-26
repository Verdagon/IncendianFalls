using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeAICapabilityUCMutSetEffect : IEffect {
  int id { get; }
  void visitIKamikazeAICapabilityUCMutSetEffect(IKamikazeAICapabilityUCMutSetEffectVisitor visitor);
}

}
