using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ISimplePresenceTriggerTTCMutSetEffect {
  int id { get; }
  void visit(ISimplePresenceTriggerTTCMutSetEffectVisitor visitor);
}

}
