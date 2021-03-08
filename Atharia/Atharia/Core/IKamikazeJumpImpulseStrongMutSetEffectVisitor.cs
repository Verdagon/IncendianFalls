using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeJumpImpulseStrongMutSetEffectVisitor {
  void visitKamikazeJumpImpulseStrongMutSetCreateEffect(KamikazeJumpImpulseStrongMutSetCreateEffect effect);
  void visitKamikazeJumpImpulseStrongMutSetDeleteEffect(KamikazeJumpImpulseStrongMutSetDeleteEffect effect);
  void visitKamikazeJumpImpulseStrongMutSetAddEffect(KamikazeJumpImpulseStrongMutSetAddEffect effect);
  void visitKamikazeJumpImpulseStrongMutSetRemoveEffect(KamikazeJumpImpulseStrongMutSetRemoveEffect effect);
}
         
}
