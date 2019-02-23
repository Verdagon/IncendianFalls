using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IExecutionStateEffectObserver {
  void OnExecutionStateEffect(IExecutionStateEffect effect);
}

}
