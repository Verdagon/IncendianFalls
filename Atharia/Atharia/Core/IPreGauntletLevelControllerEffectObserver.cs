using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IPreGauntletLevelControllerEffectObserver {
  void OnPreGauntletLevelControllerEffect(IPreGauntletLevelControllerEffect effect);
}

}
