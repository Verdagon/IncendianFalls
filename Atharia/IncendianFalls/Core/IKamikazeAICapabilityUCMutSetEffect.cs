using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeAICapabilityUCMutSetEffect {
  int id { get; }
  void visit(IKamikazeAICapabilityUCMutSetEffectVisitor visitor);
}

}
