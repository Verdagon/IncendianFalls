using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IContinueBidingImpulseEffectVisitor {
  void visitContinueBidingImpulseCreateEffect(ContinueBidingImpulseCreateEffect effect);
  void visitContinueBidingImpulseDeleteEffect(ContinueBidingImpulseDeleteEffect effect);
}

}
