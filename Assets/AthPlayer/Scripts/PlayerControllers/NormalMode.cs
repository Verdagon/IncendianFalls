using System;
using System.Collections.Generic;
using Atharia.Model;
using Domino;
using IncendianFalls;
using UnityEngine;
using static Domino.PlayerController;

namespace AthPlayer {
  public class NormalMode : IMode, IUnitEffectObserver, IUnitEffectVisitor {
    public delegate bool IAnimationsDoneAndReadyForPlayerInput();

    SuperstructureWrapper serverSS;
    Game game;
    EffectBroadcaster broadcaster;
    ShowError showError;
    IAnimationsDoneAndReadyForPlayerInput animationsDoneAndReadyForPlayerInput;
    SetHighlightLocations setHighlightLocations;

    Location maybeHoverLocation;
    bool moving = false;
    List<Location> path;

    public NormalMode(
        SuperstructureWrapper serverSS,
        Game game,
        EffectBroadcaster broadcaster,
        ShowError showError,
        IAnimationsDoneAndReadyForPlayerInput animationsDoneAndReadyForPlayerInput,
        SetHighlightLocations setHighlightLocations) {
      this.serverSS = serverSS;
      this.game = game;
      this.showError = showError;
      this.animationsDoneAndReadyForPlayerInput = animationsDoneAndReadyForPlayerInput;
      this.setHighlightLocations = setHighlightLocations;
      this.path = new List<Location>();

      this.game.player.AddObserver(broadcaster, this);
    }

    public void Destroy(bool purposeful) {
      this.game.player.RemoveObserver(broadcaster, this);
    }

    public void OnTileMouseClick(Location newLocation) {
      if (!game.player.Exists()) {
        return;
      }

      if (moving) {
        showError("Moving canceled!");
        moving = false;
        path.Clear();
        return;
      }

      Asserts.Assert(game.WaitingOnPlayerInput(), "Player not ready to act yet.");

      if (this.game.level.terrain.pattern.LocationsAreAdjacent(game.player.location, newLocation, game.level.terrain.considerCornersAdjacent)) {
        var unitAtLocation = GetUnitAt(newLocation);
        if (unitAtLocation.Exists()) {
          var error = serverSS.RequestAttack(game.id, unitAtLocation.id);
          if (error != "") {
            showError(error);
          }
          return;
        }
      }

      var sanityCheckPath = serverSS.RequestFindPath(game.id, game.player.id, newLocation);
      // If the path isnt whats already shown, then show it and bail out.
      // Maybe this could happen if the window isnt focused or something.
      if (!PathsEqual(path, sanityCheckPath)) {
        Debug.LogWarning("Clicked path doesn't equal hovered path!");
        setHighlightLocations(new SortedSet<Location>(path));
        return;
      }

      if (path.Count == 0) {
        showError("Can't go there!");
        return;
      }

      moving = true;
      Asserts.Assert(!path[0].Equals(game.player.location));
      TakeNextStep();
    }

    private void TakeNextStep() {
      Asserts.Assert(moving);
      Asserts.Assert(!path[0].Equals(game.player.location));
      var nextStep = path[0];
      path.RemoveAt(0);


      if (path.Count == 0) {
        moving = false;
        path.Clear();
      }

      var result = serverSS.RequestMove(game.id, nextStep);
      if (result != "") {
        showError(result);
        moving = false;
        path.Clear();
        return;
      }
    }

    public void Update(Location maybeHoverLocation) {
      this.maybeHoverLocation = maybeHoverLocation;

      if (moving) {
        Asserts.Assert(path.Count > 0);
        if (animationsDoneAndReadyForPlayerInput()) {
          TakeNextStep();
        }
      } else {
        // TODO: cache this somehow. Perhaps even in the level superstate? Can we even have
        // a level superstate in the client root? Dont think so... Make a thing that both we
        // and the level superstate can use.
        path = new List<Location>();
        if (maybeHoverLocation != null) {
          if (game.player.Exists()) {
            if (!maybeHoverLocation.Equals(game.player.location)) {
              if (serverSS.waitingEffects.Count == 0) {
                path = serverSS.RequestFindPath(game.id, game.player.id, maybeHoverLocation);
              }
            }
          }
        }
      }
      UpdateHighlight();
    }

    private void UpdateHighlight() {
      //if (moving) {
      // We're moving right now, just highlight that one space and the current path.
      var highlightLocations = new SortedSet<Location>(path);
      if (game.player.Exists()) {
        highlightLocations.Add(game.player.location);
      }
        if (maybeHoverLocation != null) {
          highlightLocations.Add(maybeHoverLocation);
        }
        setHighlightLocations(highlightLocations);
      //} else {
      //  // We're not moving, so highlight the path to the hover location.

      //  var highlightLocations = new SortedSet<Location>(path);
      //  highlightLocations.Add(game.player.location);
      //  setHighlightLocations(highlightLocations);
      //}
    }

    private static bool PathsEqual(List<Location> a, List<Location> b) {
      if (a.Count != b.Count) {
        return false;
      }
      for (int i = 0; i < a.Count; i++) {
        if (!a[i].Equals(b[i])) {
          return false;
        }
      }
      return true;
    }

    private Unit GetUnitAt(Location target) {
      foreach (var unit in this.game.level.units) {
        if (unit.location == target) {
          if (!unit.Alive()) {
            continue;
          }
          return unit;
        }
      }
      return Unit.Null;
    }

    public void OnUnitEffect(IUnitEffect effect) { effect.visitIUnitEffect(this); }
    public void visitUnitCreateEffect(UnitCreateEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) { }
    public void visitUnitSetEvventEffect(UnitSetEvventEffect effect) { }
    public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) { }
    public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
    public void visitUnitSetHpEffect(UnitSetHpEffect effect) { }
    public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect effect) { }
    public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) {
      if (moving) {
        UpdateHighlight();
      }
    }
  }
}
