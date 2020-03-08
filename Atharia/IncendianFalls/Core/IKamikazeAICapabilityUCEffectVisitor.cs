using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IKamikazeAICapabilityUCEffectVisitor {
  void visitKamikazeAICapabilityUCCreateEffect(KamikazeAICapabilityUCCreateEffect effect);
  void visitKamikazeAICapabilityUCDeleteEffect(KamikazeAICapabilityUCDeleteEffect effect);
  void visitKamikazeAICapabilityUCSetTargetByLocationEffect(KamikazeAICapabilityUCSetTargetByLocationEffect effect);
  void visitKamikazeAICapabilityUCSetTargetLocationCenterEffect(KamikazeAICapabilityUCSetTargetLocationCenterEffect effect);
}

}
