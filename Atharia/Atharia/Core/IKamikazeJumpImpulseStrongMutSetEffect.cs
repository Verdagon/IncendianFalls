using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeJumpImpulseStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIKamikazeJumpImpulseStrongMutSetEffect(IKamikazeJumpImpulseStrongMutSetEffectVisitor visitor);
}

}
