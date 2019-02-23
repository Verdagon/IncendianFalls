using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IArmorMutSetEffect {
  int id { get; }
  void visit(IArmorMutSetEffectVisitor visitor);
}

}
