using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IBlastRodEffect : IEffect {
  int id { get; }
  void visitIBlastRodEffect(IBlastRodEffectVisitor visitor);
}
       
}
