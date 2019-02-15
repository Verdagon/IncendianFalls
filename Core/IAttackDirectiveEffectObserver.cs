using System;
using System.Collections.Generic;

namespace Atharia.Model {

public interface IAttackDirectiveEffectObserver {
  void OnAttackDirectiveEffect(IAttackDirectiveEffect effect);
}

}
