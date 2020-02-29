using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IMudTTCEffectVisitor {
  void visitMudTTCCreateEffect(MudTTCCreateEffect effect);
  void visitMudTTCDeleteEffect(MudTTCDeleteEffect effect);
}

}
