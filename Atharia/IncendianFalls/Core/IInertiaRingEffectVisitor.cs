using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IInertiaRingEffectVisitor {
  void visitInertiaRingCreateEffect(InertiaRingCreateEffect effect);
  void visitInertiaRingDeleteEffect(InertiaRingDeleteEffect effect);
}

}
