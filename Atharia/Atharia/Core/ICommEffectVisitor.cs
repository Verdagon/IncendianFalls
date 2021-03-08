using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICommEffectVisitor {
  void visitCommCreateEffect(CommCreateEffect effect);
  void visitCommDeleteEffect(CommDeleteEffect effect);
}

}
