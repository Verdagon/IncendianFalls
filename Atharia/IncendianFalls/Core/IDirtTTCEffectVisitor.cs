using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDirtTTCEffectVisitor {
  void visitDirtTTCCreateEffect(DirtTTCCreateEffect effect);
  void visitDirtTTCDeleteEffect(DirtTTCDeleteEffect effect);
}

}
