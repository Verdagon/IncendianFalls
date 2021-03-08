using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ILakeLevelControllerEffectObserver {
  void OnLakeLevelControllerEffect(ILakeLevelControllerEffect effect);
}

}
