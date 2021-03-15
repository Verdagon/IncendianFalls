using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IDeathTriggerUCMutSetEffect : IEffect {
  int id { get; }
  void visitIDeathTriggerUCMutSetEffect(IDeathTriggerUCMutSetEffectVisitor visitor);
}

}
