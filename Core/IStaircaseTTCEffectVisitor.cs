using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IStaircaseTTCEffectVisitor {
  void visitStaircaseTTCCreateEffect(StaircaseTTCCreateEffect effect);
  void visitStaircaseTTCDeleteEffect(StaircaseTTCDeleteEffect effect);
  void visitStaircaseTTCSetDestinationLevelEffect(StaircaseTTCSetDestinationLevelEffect effect);
  void visitStaircaseTTCSetDestinationLevelPortalIndexEffect(StaircaseTTCSetDestinationLevelPortalIndexEffect effect);
}

}
