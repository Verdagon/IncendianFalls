using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IArmorStrongMutSetEffect {
  int id { get; }
  void visit(IArmorStrongMutSetEffectVisitor visitor);
}

}
