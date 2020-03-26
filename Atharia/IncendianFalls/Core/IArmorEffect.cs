using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IArmorEffect : IEffect {
  int id { get; }
  void visitIArmorEffect(IArmorEffectVisitor visitor);
}
       
}
