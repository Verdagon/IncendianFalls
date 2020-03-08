using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDoomedUCEffectVisitor {
  void visitDoomedUCCreateEffect(DoomedUCCreateEffect effect);
  void visitDoomedUCDeleteEffect(DoomedUCDeleteEffect effect);
}

}
