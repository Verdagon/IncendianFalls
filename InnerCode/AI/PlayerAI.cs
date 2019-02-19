using System;
using Atharia.Model;

namespace IncendianFalls {
  public class PlayerAI {
    public static bool FollowMoveDirective(
        Game game,
        LiveUnitByLocationMap liveUnitByLocationMap,
        Unit unit) {
      Asserts.Assert(unit.directive is MoveDirectiveAsIDirective move);
      var directive = (unit.directive as MoveDirectiveAsIDirective).obj;

      if (!Actions.CanStep(game, liveUnitByLocationMap, game.player, directive.path[0])) {
        unit.directive.Delete();
        Console.WriteLine("Blocked!");
        return false;
      }

      Actions.Step(game, liveUnitByLocationMap, game.player, directive.path[0]);
      directive.path.RemoveAt(0);

      if (directive.path.Count == 0) {
        directive.Delete();
      }

      return true;
    }
  }
}
