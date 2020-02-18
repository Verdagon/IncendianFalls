﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domino {
  public class UnitDescription {
    public readonly object tag;
    public readonly DominoDescription dominoDescription;
    public readonly ExtrudedSymbolDescription faceSymbolDescription;
    public readonly List<KeyValuePair<int, ExtrudedSymbolDescription>> detailSymbolDescriptionById;
    public readonly float hpRatio;
    public readonly float mpRatio;

    public UnitDescription(
        object tag,
        DominoDescription dominoDescription,
        ExtrudedSymbolDescription faceSymbolDescription,
        List<KeyValuePair<int, ExtrudedSymbolDescription>> detailSymbolDescriptionById,
        float hpRatio,
        float mpRatio) {
      this.tag = tag;
      this.dominoDescription = dominoDescription;
      this.faceSymbolDescription = faceSymbolDescription;
      this.detailSymbolDescriptionById = detailSymbolDescriptionById;
      this.hpRatio = hpRatio;
      this.mpRatio = mpRatio;
    }


    public override bool Equals(object obj) {
      if (!(obj is UnitDescription))
        return false;
      UnitDescription that = obj as UnitDescription;
      if (tag != that.tag)
        return false;
      if (!dominoDescription.Equals(that.dominoDescription))
        return false;
      if (!faceSymbolDescription.Equals(that.faceSymbolDescription))
        return false;
      if (detailSymbolDescriptionById.Count != that.detailSymbolDescriptionById.Count)
        return false;
      for (int i = 0; i < detailSymbolDescriptionById.Count; i++) {
        if (detailSymbolDescriptionById[i].Key != that.detailSymbolDescriptionById[i].Key)
          return false;
        if (!detailSymbolDescriptionById[i].Value.Equals(that.detailSymbolDescriptionById[i].Value))
          return false;
      }
      if (hpRatio != that.hpRatio)
        return false;
      if (mpRatio != that.mpRatio)
        return false;
      return true;
    }
    public override int GetHashCode() {
      int hashCode = 0;
      hashCode += 13 * tag.GetHashCode();
      hashCode += 17 * dominoDescription.GetHashCode();
      hashCode += 33 * faceSymbolDescription.GetHashCode();
      hashCode += 53 * detailSymbolDescriptionById.Count;
      foreach (var detailSymbolDescription in detailSymbolDescriptionById) {
        hashCode += 67 * detailSymbolDescription.Key + 79 * detailSymbolDescription.Value.GetHashCode();
      }
      hashCode += 87 * (int)(hpRatio * 100);
      hashCode += 103 * (int)(mpRatio * 100);
      return hashCode;
    }
  }

  public class UnitView : MonoBehaviour {
    public static readonly float HOP_DURATION = .3f;

    Instantiator instantiator;

    ITimer timer;

    private bool initialized = false;

    // The main object that lives in world space. It has no rotation or scale,
    // just a translation to the center of the tile the unit is in.
    // public GameObject gameObject; (provided by unity)

    // Object for slightly translating the unit.
    // Lives inside this.gameObject.
    private GameObject offsetter;

    // The object that contains the details bar and anything that's aligned to
    // the domino (details bar, symbol, everything else).
    // Inside the offsetter; the offsetter exists to slightly translate this
    // such as when we're lunging.
    private GameObject body;

    // The domino. Can either be the large or small one.
    // Lives inside this.body.
    private DominoView dominoView;

    // The symbol on the domino's field.
    // Lives inside this.body.
    private SymbolView faceSymbolView;

    // An invisible object aligned with the top of the unit, which will contain
    // any detail symbols.
    // Lives inside this.body.
    // Nullable. Only non-null when there's details.
    private SymbolBarView symbolBarView;

    // An invisible object aligned with the top of the unit, which will contain
    // any detail symbols.
    // Lives inside this.body.
    // Nullable. Only non-null when there's details.
    private MeterView hpMeterView;

    // An invisible object aligned with the top of the unit, which will contain
    // any detail symbols.
    // Lives inside this.body.
    // Nullable. Only non-null when there's details.
    private MeterView mpMeterView;

    // The unadjusted position of the unit. By unadjusted, we mean that
    // we havent offsetted it (like in a lunge) and havent moved it forward
    // (to compensate for the lean).
    // This should probably just be the center of the tile below us.
    private Vector3 basePosition;

    public void Init(
        Instantiator instantiator,
        ITimer timer,
        Vector3 basePosition,
        UnitDescription unitDescription) {
      this.instantiator = instantiator;
      this.timer = timer;
      this.basePosition = basePosition;

      gameObject.transform.position = basePosition;

      offsetter = new GameObject();
      offsetter.transform.SetParent(gameObject.transform, false);

      body = new GameObject();
      body.transform.SetParent(offsetter.transform, false);

      body.transform.localRotation = Quaternion.AngleAxis(50, new Vector3(1, 0, 0));
      body.transform.localScale = new Vector3(.8f, .8f, .8f);
      body.transform.localPosition = new Vector3(0, 0, -.25f);

      dominoView = instantiator.CreateDominoView(unitDescription.dominoDescription);
      dominoView.gameObject.transform.SetParent(body.transform, false);

      faceSymbolView = instantiator.CreateSymbolView(false, unitDescription.faceSymbolDescription);
      faceSymbolView.gameObject.transform.FromMatrix(
          ScaleToAndCenterInTile(unitDescription.dominoDescription.large));
      faceSymbolView.gameObject.transform.SetParent(body.transform, false);

      if (unitDescription.detailSymbolDescriptionById.Count != 0) {
        symbolBarView = MakeSymbolBarView(instantiator, unitDescription.detailSymbolDescriptionById, unitDescription.dominoDescription.large);
        symbolBarView.gameObject.transform.SetParent(body.transform, false);
      }

      bool showHpMeter = unitDescription.hpRatio < .999f;
      if (showHpMeter) {
        hpMeterView = instantiator.CreateMeterView(unitDescription.hpRatio, Color.green, Color.red);
        hpMeterView.gameObject.transform.FromMatrix(MakeMeterViewTransform(0));
        hpMeterView.transform.SetParent(body.transform, false);
      }

      bool showMpMeter = unitDescription.mpRatio < .999f;
      if (showMpMeter) {
        int position = (showHpMeter ? 1 : 0);
        mpMeterView = instantiator.CreateMeterView(unitDescription.mpRatio, Color.blue, Color.white);
        mpMeterView.gameObject.transform.FromMatrix(MakeMeterViewTransform(position));
        mpMeterView.transform.SetParent(body.transform, false);
      }

      initialized = true;
    }

    public void SetUnitViewActive(bool enabled) {
      gameObject.SetActive(false);
    }

    public void SetDescription(UnitDescription newUnitDescription) {
      dominoView.SetDescription(newUnitDescription.dominoDescription);
      faceSymbolView.SetDescription(newUnitDescription.faceSymbolDescription);

      if (symbolBarView == null) {
        if (newUnitDescription.detailSymbolDescriptionById.Count == 0) {
          // Dont do anything, its already gone
        } else {
          symbolBarView = MakeSymbolBarView(instantiator, newUnitDescription.detailSymbolDescriptionById, newUnitDescription.dominoDescription.large);
          symbolBarView.gameObject.transform.SetParent(body.transform, false);
        }
      } else {
        if (newUnitDescription.detailSymbolDescriptionById.Count == 0) {
          symbolBarView.Destruct();
          symbolBarView = null;
        } else {
          symbolBarView.SetDescriptions(newUnitDescription.detailSymbolDescriptionById);
        }
      }

      bool shouldShowHpMeter = newUnitDescription.hpRatio < .999f;
      bool didShowHpMeter = (hpMeterView != null);
      bool showingHpMeterChanged = (shouldShowHpMeter != didShowHpMeter);
      if (shouldShowHpMeter) {
        if (hpMeterView == null) {
          hpMeterView = instantiator.CreateMeterView(newUnitDescription.hpRatio, Color.green, Color.red);
          hpMeterView.gameObject.transform.FromMatrix(MakeMeterViewTransform(0));
          hpMeterView.transform.SetParent(body.transform, false);
        } else {
          hpMeterView.ratio = newUnitDescription.hpRatio;
        }
      } else {
        if (hpMeterView == null) {
          // Do nothing
        } else {
          hpMeterView.Destruct();
          hpMeterView = null;
        }
      }

      bool shouldShowMpMeter = newUnitDescription.mpRatio < .999f;
      int mpMeterPosition = (shouldShowHpMeter ? 1 : 0);
      if (shouldShowMpMeter) {
        if (mpMeterView == null) {
          mpMeterView = instantiator.CreateMeterView(newUnitDescription.mpRatio, Color.blue, Color.white);
          mpMeterView.gameObject.transform.FromMatrix(MakeMeterViewTransform(mpMeterPosition));
          mpMeterView.transform.SetParent(body.transform, false);
        } else {
          mpMeterView.ratio = newUnitDescription.mpRatio;
        }
      } else {
        if (mpMeterView == null) {
          // Do nothing
        } else {
          mpMeterView.Destruct();
          mpMeterView = null;
        }
      }
      if (shouldShowMpMeter && showingHpMeterChanged) {
        mpMeterView.gameObject.transform.FromMatrix(MakeMeterViewTransform(mpMeterPosition));
      }
    }

    private static Matrix4x4 MakeMeterViewTransform(int position) {
      MatrixBuilder hpMeterTransform = new MatrixBuilder(Matrix4x4.identity);
      hpMeterTransform.Scale(new Vector3(1, 1, .1f));
      hpMeterTransform.Translate(new Vector3(-.5f, 0, 0));
      hpMeterTransform.Rotate(Quaternion.AngleAxis(270, Vector3.right));
      hpMeterTransform.Translate(new Vector3(0, 0, -.1f));
      hpMeterTransform.Translate(new Vector3(0, .1f * position, 0));
      return hpMeterTransform.matrix;
    }

    private static SymbolBarView MakeSymbolBarView(
        Instantiator instantiator,
        List<KeyValuePair<int, ExtrudedSymbolDescription>> symbolsIdsAndDescriptions,
        bool large) {
      SymbolBarView symbolBarView =
          instantiator.CreateSymbolBarView(symbolsIdsAndDescriptions);

      MatrixBuilder symbolBarMatrixBuilder = new MatrixBuilder(Matrix4x4.identity);

      symbolBarMatrixBuilder.Translate(new Vector3(0, 0, -0.1f));
      if (large) {
        symbolBarMatrixBuilder.Translate(new Vector3(0, 1.5f, 0));
      } else {
        symbolBarMatrixBuilder.Translate(new Vector3(0, 1f, 0));
      }
      symbolBarView.transform.FromMatrix(symbolBarMatrixBuilder.matrix);

      return symbolBarView;
    }

    public void DestroyUnit() {
      Destroy(this.gameObject);
    }

    public void Start() {
      if (!initialized) {
        throw new Exception("UnitView component not initialized!");
      }
    }

    public void HopTo(Vector3 newBasePosition) {
      Vector3 oldBasePosition = basePosition;
      basePosition = newBasePosition;

      gameObject.transform.localPosition = newBasePosition;

      StartHopAnimation(HOP_DURATION, newBasePosition - oldBasePosition, 0.5f);
    }

    private MovementAnimator GetOrCreateMovementAnimator() {
      var animator = offsetter.GetComponent<MovementAnimator>();
      if (animator == null) {
        animator = offsetter.AddComponent<MovementAnimator>() as MovementAnimator;
      }
      return animator;
    }

    private void StartHopAnimation(float duration, Vector3 offset, float height) {
      // We technically just moved the unit to the new position, but we need to compensate
      // for it here to make it look like it's still back there and slowly transitioning.
      var anim = GetOrCreateMovementAnimator();
      anim.transformAnimation =
          new ComposeMatrix4x4Animation(
              anim.transformAnimation,
              new ComposeMatrix4x4Animation(
                  new ConstantMatrix4x4Animation(Matrix4x4.Translate(-offset)),
                  new HopAnimation(
                      Time.time, Time.time + duration, offset, height)));
    }

    public void Lunge(Vector3 offset) {
      StartLungeAnimation(.15f, offset);
    }

    private void StartLungeAnimation(float duration, Vector3 offset) {
      var anim = GetOrCreateMovementAnimator();
      anim.transformAnimation =
          new ComposeMatrix4x4Animation(
              anim.transformAnimation,
              new LungeAnimation(Time.time, Time.time + duration, offset));
    }

    public void ShowRune(ExtrudedSymbolDescription runeSymbolDescription) {
      var symbolView = instantiator.CreateSymbolView(false, runeSymbolDescription);
      symbolView.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
      symbolView.transform.localScale = new Vector3(1, 1, .1f);
      if (dominoView.large) {
        symbolView.transform.localPosition = new Vector3(0, 1f, -.2f);
      } else {
        symbolView.transform.localPosition = new Vector3(0, 0.5f, -.2f);
      }
      symbolView.transform.SetParent(body.transform, false);
      symbolView.FadeInThenOut(0.1f, 0.4f);
      //timer.ScheduleTimer(1.0f, () => symbolView.Destruct());
    }

    public void Die(float duration) {
      StartDieAnimation(duration);
    }

    private void StartDieAnimation(float duration) {
      dominoView.Fade(duration);

      faceSymbolView.Fade(duration);

      if (hpMeterView != null) {
        hpMeterView.Fade(duration);
      }

      var anim = GetOrCreateMovementAnimator();
      anim.transformAnimation =
          new ComposeMatrix4x4Animation(
              anim.transformAnimation,
              new ClampMatrix4x4Animation(Time.time, Time.time + duration,
                  new LinearMatrix4x4Animation(Time.time, Time.time + duration,
                      Matrix4x4.identity,
                      Matrix4x4.Translate(new Vector3(0, -.05f, 0)))));
    }

    static Matrix4x4 ScaleToAndCenterInTile(bool inLargeTile) {
      MatrixBuilder transform = new MatrixBuilder(Matrix4x4.identity);

      // No idea why we need these. Something to do with how unity is retarded with
      // importing .obj files.
      transform.Scale(new Vector3(-1, 1, 1));
      transform.Rotate(Quaternion.AngleAxis(180, Vector3.up));

      // The tile is 0.7f x 0.7f x 0.1f.
      // We want to maintain the ratio between X and Y.

      // The symbol is currently ((-.5,-.5), (-1, 0), (.5,.5))
      // We want it to be ((-.35,.35), (-.1,.1), (.35,.35))

      // We want a .1 thickness symbol.
      transform.Scale(new Vector3(1, 1, .1f));

      // Now, since we want to fit things in 0.7,0.7 instead of 1.0,1.0, scale it down
      // and nudge it over a bit.
      transform.Scale(new Vector3(0.7f, 0.7f, 1.0f));

      // The symbol is now ((-.35,-.35), (-.1, 0), (.35,.35))

      transform.Translate(new Vector3(0, 0, -.05f));

      transform.Translate(new Vector3(0, 0.5f, 0));


      //// Tiles (0, 0) is at the center of the bottom edge, so we have to move
      //// x over by 0.5 and z over by 0.05.
      //transform.Translate(new Vector3(-0.5f, 0, -0.05f));
      //// Now it's centered inside -0.35,0.15 to 0.35,0.85.

      if (inLargeTile) {
        // If we're in a large tile then bump it up by 0.5 in the y direction.
        transform.Translate(new Vector3(0, 0.5f, 0));
      }

      //// Our ascii models don't agree with unity's left-handedness so we rotate around
      //// the Y axis here.
      //transform.Rotate(Quaternion.AngleAxis(180, new Vector3(0, 1, 0)));

      return transform.matrix;
    }
  }
}