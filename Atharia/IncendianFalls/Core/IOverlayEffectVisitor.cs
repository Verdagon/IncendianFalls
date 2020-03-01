using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IOverlayEffectVisitor {
  void visitOverlayCreateEffect(OverlayCreateEffect effect);
  void visitOverlayDeleteEffect(OverlayDeleteEffect effect);
}

}
