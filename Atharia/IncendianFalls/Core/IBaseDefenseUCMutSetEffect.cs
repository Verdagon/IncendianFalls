using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IBaseDefenseUCMutSetEffect {
  int id { get; }
  void visit(IBaseDefenseUCMutSetEffectVisitor visitor);
}

}
