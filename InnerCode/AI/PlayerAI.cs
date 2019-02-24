using System;
using Atharia.Model;

namespace IncendianFalls {
  public class PlayerAI {
    public static bool FollowMoveDirective(
        Game game,
        LiveUnitByLocationMap liveUnitByLocationMap,
        Unit unit) {
      var idirective = unit.components.GetOnlyIDirectiveUCOrNull();
      Asserts.Assert(idirective is MoveDirectiveUCAsIDirectiveUC move);
      var directive = (idirective as MoveDirectiveUCAsIDirectiveUC).obj;

      if (!Actions.CanStep(game, liveUnitByLocationMap, game.player, directive.path[0])) {
        unit.components.ClearAllIDirectiveUC();
        directive.Destruct();
        game.root.logger.Info("Blocked!");
        return false;
      }

      Actions.Step(game, liveUnitByLocationMap, game.player, directive.path[0]);
      directive.path.RemoveAt(0);

      if (directive.path.Count == 0) {
        unit.components.Remove(directive.AsIUnitComponent());
        directive.Destruct();
      }

      return true;
    }
  }
}
