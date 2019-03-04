using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ITimeScriptDirectiveUCEffectVisitor {
  void visitTimeScriptDirectiveUCCreateEffect(TimeScriptDirectiveUCCreateEffect effect);
  void visitTimeScriptDirectiveUCDeleteEffect(TimeScriptDirectiveUCDeleteEffect effect);
}

}
