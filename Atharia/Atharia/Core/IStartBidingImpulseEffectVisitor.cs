using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IStartBidingImpulseEffectVisitor {
  void visitStartBidingImpulseCreateEffect(StartBidingImpulseCreateEffect effect);
  void visitStartBidingImpulseDeleteEffect(StartBidingImpulseDeleteEffect effect);
}

}
