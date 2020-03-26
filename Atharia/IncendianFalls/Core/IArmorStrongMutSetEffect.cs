using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IArmorStrongMutSetEffect : IEffect {
  int id { get; }
  void visitIArmorStrongMutSetEffect(IArmorStrongMutSetEffectVisitor visitor);
}

}
