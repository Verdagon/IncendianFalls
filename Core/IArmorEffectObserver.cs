using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IArmorEffectObserver {
  void OnArmorEffect(IArmorEffect effect);
}

}
