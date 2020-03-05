using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISorcerousUCEffectVisitor {
  void visitSorcerousUCCreateEffect(SorcerousUCCreateEffect effect);
  void visitSorcerousUCDeleteEffect(SorcerousUCDeleteEffect effect);
  void visitSorcerousUCSetMpEffect(SorcerousUCSetMpEffect effect);
  void visitSorcerousUCSetMaxMpEffect(SorcerousUCSetMaxMpEffect effect);
}

}
