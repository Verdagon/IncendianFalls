using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IWatEffectVisitor {
  void visitWatCreateEffect(WatCreateEffect effect);
  void visitWatDeleteEffect(WatDeleteEffect effect);
}

}
