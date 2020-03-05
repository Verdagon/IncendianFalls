using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseOffenseUCMutSetEffect {
  int id { get; }
  void visit(IBaseOffenseUCMutSetEffectVisitor visitor);
}

}
