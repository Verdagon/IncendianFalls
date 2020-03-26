using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public interface IGameEffectVisitor {
  void visitGameCreateEffect(GameCreateEffect effect);
  void visitGameDeleteEffect(GameDeleteEffect effect);
  void visitGameSetPlayerEffect(GameSetPlayerEffect effect);
  void visitGameSetLevelEffect(GameSetLevelEffect effect);
  void visitGameSetTimeEffect(GameSetTimeEffect effect);
  void visitGameSetActionNumEffect(GameSetActionNumEffect effect);
  void visitGameSetInstructionsEffect(GameSetInstructionsEffect effect);
  void visitGameSetHideInputEffect(GameSetHideInputEffect effect);
}

}
