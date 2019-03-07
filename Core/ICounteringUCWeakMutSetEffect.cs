using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface ICounteringUCWeakMutSetEffect {
  int id { get; }
  void visit(ICounteringUCWeakMutSetEffectVisitor visitor);
}

}
