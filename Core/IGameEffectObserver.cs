using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IGameEffectObserver {
  void OnGameEffect(IGameEffect effect);
}

}
