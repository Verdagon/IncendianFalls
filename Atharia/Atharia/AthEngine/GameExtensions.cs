using System;
using Atharia.Model;

namespace Atharia.Model {
  public enum WorldStateType {
    kBetweenUnits = 0,
    kBeforePlayerInput = 4,
  }

  public static class GameExtensions {
    public static bool WaitingOnPlayerInput(this Game game) {
      return game.actingUnit.Exists() &&
        game.actingUnit.NullableIs(game.player) &&
          game.player.Alive() &&
          game.player.nextActionTime == game.time;
    }
    public static void AddEvent(this Game game, IGameEvent e) {
      game.evvent = e;
      game.evvent = NullIGameEvent.Null;
    }

    public static void EnterCinematic(this Game game) {
      if (game.hideInput) {
        game.root.logger.Error("Entering cinematic but we were already in one!");
      }
      game.hideInput = true;
    }

    public static void ExitCinematic(this Game game) {
      if (!game.hideInput) {
        game.root.logger.Error("Exiting cinematic but we werent in one!");
      }
      game.hideInput = false;
    }

    public static void ShowAside(this Game game, string text) {
      game.ShowAside("narrator", text);
    }

    public static void WaitForCamera(this Game game) {
      game.AddEvent(new WaitForCameraEvent().AsIGameEvent());
      game.actionNum++;
    }

    public static void Wait(this Game game, long ms) {
      game.AddEvent(new WaitEvent((int)ms).AsIGameEvent());
      game.actionNum++;
    }

    public static void FlyCameraTo(this Game game, int flightTimeMs, Location location, bool waitFor = true) {
      game.AddEvent(new FlyCameraEvent(location, new Vec3(0, -16, 8), flightTimeMs).AsIGameEvent());
      game.actionNum++;
      if (waitFor) {
        // Gotta have this after the above actionNum++ so this waits on that camera movement.
        game.WaitForCamera();
        game.actionNum++;
      }
    }

    public static void ShowAside(this Game game, string speakerRole, string text) {
      game.comms.Add(
        game.root.EffectCommCreate(
          new AsideCommTemplate().AsICommTemplate(),
          new CommActionImmList(),
          new CommTextImmList(new CommText(speakerRole, text))));
    }

    public static void ShowDialogue(this Game game, string speakerRole, string text, string continueButtonText) {
      game.comms.Add(
        game.root.EffectCommCreate(
          new DialogueCommTemplate().AsICommTemplate(),
          new CommActionImmList(new CommAction(continueButtonText, "")),
          new CommTextImmList(new CommText(speakerRole, text))));
      game.actionNum++;
    }

    public static void ShowComm(this Game game, string continueButtonText, string text) {
      game.comms.Add(
        game.root.EffectCommCreate(
          new NormalCommTemplate().AsICommTemplate(),
          new CommActionImmList(new CommAction(continueButtonText, "")),
          new CommTextImmList(new CommText("narrator", text))));
      game.actionNum++;
    }

    public static void ShowDramatic(this Game game, string speakerRole, string text, bool isObscuring = false) {
      game.comms.Add(
        game.root.EffectCommCreate(
          new DramaticCommTemplate(isObscuring).AsICommTemplate(),
          new CommActionImmList(new CommAction("...", "")),
          new CommTextImmList(new CommText(speakerRole, text))));
      game.actionNum++;
    }

    public static void ShowDramatic(this Game game, CommText[] texts, bool isObscuring = false) {
      game.comms.Add(
        game.root.EffectCommCreate(
          new DramaticCommTemplate(isObscuring).AsICommTemplate(),
          new CommActionImmList(new CommAction("...", "")),
          new CommTextImmList(texts)));
      game.actionNum++;
    }
  }
}
