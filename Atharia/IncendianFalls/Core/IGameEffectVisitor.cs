using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGameEffectVisitor {
  void visitGameCreateEffect(GameCreateEffect effect);
  void visitGameDeleteEffect(GameDeleteEffect effect);
  void visitGameSetPlayerEffect(GameSetPlayerEffect effect);
  void visitGameSetLevelEffect(GameSetLevelEffect effect);
  void visitGameSetInstructionsEffect(GameSetInstructionsEffect effect);
  void visitGameSetTimeEffect(GameSetTimeEffect effect);
}

}
