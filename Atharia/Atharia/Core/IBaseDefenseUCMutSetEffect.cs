using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseDefenseUCMutSetEffect : IEffect {
  int id { get; }
  void visitIBaseDefenseUCMutSetEffect(IBaseDefenseUCMutSetEffectVisitor visitor);
}

}
