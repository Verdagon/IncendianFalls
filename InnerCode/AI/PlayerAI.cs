using System;
using Atharia.Model;

namespace IncendianFalls {
  public class PlayerAI {
    public static bool FollowMoveDirective(
        Game game,
        Superstate superstate,
        Unit unit) {
      var idirective = unit.components.GetOnlyIDirectiveUCOrNull();
      Asserts.Assert(idirective is MoveDirectiveUCAsIDirectiveUC move);
      var directive = (idirective as MoveDirectiveUCAsIDirectiveUC).obj;

      if (!Actions.CanStep(game, superstate, game.player, directive.path[0])) {
        unit.ClearDirective();
        game.root.logger.Info("Blocked!");
        return false;
      }

      Actions.Step(game, superstate, game.player, directive.path[0]);
      directive.path.RemoveAt(0);

      if (directive.path.Count == 0) {
        unit.ClearDirective();
      }

      return true;
    }
  }
}
