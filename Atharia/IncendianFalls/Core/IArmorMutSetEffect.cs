using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IArmorMutSetEffect : IEffect {
  int id { get; }
  void visitIArmorMutSetEffect(IArmorMutSetEffectVisitor visitor);
}

}
