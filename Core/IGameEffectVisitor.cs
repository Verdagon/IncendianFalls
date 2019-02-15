using System;
using System.Collections.Generic;

namespace Atharia.Model {
public interface IGameEffectVisitor {
  void visitGameCreateEffect(GameCreateEffect effect);
  void visitGameDeleteEffect(GameDeleteEffect effect);
  void visitGameSetPlayerEffect(GameSetPlayerEffect effect);
  void visitGameSetLevelEffect(GameSetLevelEffect effect);
  void visitGameSetTimeEffect(GameSetTimeEffect effect);
}

}
