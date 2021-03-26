using System;
using System.Collections.Generic;
using UnityEngine;

namespace Domino {
  public class TileDescription {
    public readonly float elevationStepHeight;
    public readonly float tileRotationDegrees;
    public readonly int depth; // basically elevation
    public readonly ExtrudedSymbolDescription tileSymbolDescription;
    public readonly ExtrudedSymbolDescription maybeOverlaySymbolDescription;
    public readonly ExtrudedSymbolDescription maybeFeatureSymbolDescription;
    public readonly List<(ulong, ExtrudedSymbolDescription)> itemSymbolDescriptionByItemId;

    public TileDescription(
        float elevationStepHeight,
        float tileRotationDegrees,
        int depth,
        ExtrudedSymbolDescription tileSymbolDescription,
        ExtrudedSymbolDescription maybeOverlaySymbolDescription,
        ExtrudedSymbolDescription maybeFeatureSymbolDescription,
        List<(ulong, ExtrudedSymbolDescription)> itemSymbolDescriptionByItemId) {
      this.elevationStepHeight = elevationStepHeight;
      this.tileRotationDegrees = tileRotationDegrees;
      this.depth = depth;
      this.tileSymbolDescription = tileSymbolDescription;
      this.maybeOverlaySymbolDescription = maybeOverlaySymbolDescription;
      this.maybeFeatureSymbolDescription = maybeFeatureSymbolDescription;
      this.itemSymbolDescriptionByItemId = itemSymbolDescriptionByItemId;
    }

    public TileDescription WithTileSymbolDescription(ExtrudedSymbolDescription newTileSymbolDescription) {
      return new TileDescription(
        elevationStepHeight,
        tileRotationDegrees,
        depth,
        newTileSymbolDescription,
        maybeOverlaySymbolDescription,
        maybeFeatureSymbolDescription,
        itemSymbolDescriptionByItemId);
    }

    public override bool Equals(object obj) {
      if (!(obj is TileDescription))
        return false;
      TileDescription that = obj as TileDescription;
      if (elevationStepHeight != that.elevationStepHeight)
        return false;
      if (tileRotationDegrees != that.tileRotationDegrees)
        return false;
      if (depth != that.depth)
        return false;
      if (!tileSymbolDescription.Equals(that.tileSymbolDescription))
        return false;
      if ((maybeOverlaySymbolDescription != null) != (that.maybeOverlaySymbolDescription != null))
        return false;
      if (maybeOverlaySymbolDescription != null && !maybeOverlaySymbolDescription.Equals(that.maybeOverlaySymbolDescription))
        return false;
      if ((maybeFeatureSymbolDescription != null) != (that.maybeFeatureSymbolDescription != null))
        return false;
      if (maybeFeatureSymbolDescription != null && !maybeFeatureSymbolDescription.Equals(that.maybeFeatureSymbolDescription))
        return false;
      if (itemSymbolDescriptionByItemId.Count != that.itemSymbolDescriptionByItemId.Count)
        return false;
      for (int i = 0; i < itemSymbolDescriptionByItemId.Count; i++) {
        if (itemSymbolDescriptionByItemId[i].Item1 != that.itemSymbolDescriptionByItemId[i].Item1)
          return false;
        if (!itemSymbolDescriptionByItemId[i].Item2.Equals(that.itemSymbolDescriptionByItemId[i].Item2))
          return false;
      }
      return true;
    }
    public override int GetHashCode() {
      int hashCode = 0;
      hashCode += 27 * elevationStepHeight.GetHashCode();
      hashCode += 31 * tileRotationDegrees.GetHashCode();
      hashCode += 37 * depth.GetHashCode();
      hashCode += 41 * tileSymbolDescription.GetHashCode();
      if (maybeOverlaySymbolDescription != null)
        hashCode += 47 * maybeOverlaySymbolDescription.GetHashCode();
      if (maybeFeatureSymbolDescription != null)
        hashCode += 53 * maybeFeatureSymbolDescription.GetHashCode();
      hashCode += 67 * itemSymbolDescriptionByItemId.Count;
      foreach (var entry in itemSymbolDescriptionByItemId) {
        hashCode += 87 * entry.Item1.GetHashCode() + 93 * entry.Item2.GetHashCode();
      }
      return hashCode;
    }
  }

  public class TileView : MonoBehaviour {
    private bool initialized = false;
    public bool alive {  get { return initialized;  } }

    private IClock clock;
    private ITimer timer;

    private List<SymbolView> tileSymbolViews = new List<SymbolView>();

    private SymbolView overlaySymbolView;

    private SymbolView featureSymbolView;

    private List<(ulong, SymbolView)> itemSymbolViewByItemId = new List<(ulong, SymbolView)>();

    Instantiator instantiator;

    private float elevationStepHeight;
    private IVector4Animation topColor;
    private IVector4Animation sideColor;
    private ExtrudedSymbolDescription maybeFeature;
    private ExtrudedSymbolDescription maybeOverlay;
    private string tileSymbolId;
    private float tileRotationDegrees;
    private float tileScale;
    private OutlineMode tileOutlineMode;
    private IVector4Animation tileOutlineColor;

    // We have timers active to destroy these when theyre done, but we might
    // also destroy them if we need to Destruct fast.
    private List<KeyValuePair<SymbolView, int>> transientPrismSymbolsAndTimerIds;

    public void Init(
      IClock clock,
      ITimer timer,
        Instantiator instantiator,
        Vector3 basePosition,
        TileDescription initialDescription) {
      this.clock = clock;
      this.timer = timer;
      this.instantiator = instantiator;

      transientPrismSymbolsAndTimerIds = new List<KeyValuePair<SymbolView, int>>();

      gameObject.transform.localPosition = basePosition;

      initialized = true;
      
      tileSymbolId = initialDescription.tileSymbolDescription.symbol.symbolId.name;
      tileRotationDegrees = initialDescription.tileSymbolDescription.symbol.rotationDegrees;
      elevationStepHeight = initialDescription.elevationStepHeight;
      tileScale = initialDescription.tileSymbolDescription.symbol.scale;
      tileOutlineMode = initialDescription.tileSymbolDescription.symbol.withOutline;
      tileOutlineColor = initialDescription.tileSymbolDescription.symbol.outlineColor;
      SetFrontColor(initialDescription.tileSymbolDescription.symbol.frontColor);
      SetSidesColor(initialDescription.tileSymbolDescription.sidesColor);
      // This is when the tile views are actually made
      SetDepth(initialDescription.depth);
      SetOverlay(initialDescription.maybeOverlaySymbolDescription);
      SetFeature(initialDescription.maybeFeatureSymbolDescription);
      foreach (var (itemId, itemDescription) in initialDescription.itemSymbolDescriptionByItemId) {
        AddItem(itemId, itemDescription);
      }
    }

    public void DestroyTile() {
      initialized = false;

      foreach (var transientRuneAndTimerId in transientPrismSymbolsAndTimerIds) {
        var rune = transientRuneAndTimerId.Key;
        var timerId = transientRuneAndTimerId.Value;
        timer.CancelTimer(timerId);
        rune.Destruct();
      }

      Destroy(this.gameObject);
    }

    private void SetTileOrPrismTransform(SymbolView tileSymbolView, float elevationStepHeight, float rotationDegrees, int elevation, int height) {
      // No idea why we need the -90 or the - before the rotation. It has to do with
      // unity's infuriating mishandling of .obj file imports.
      tileSymbolView.gameObject.transform.localRotation =
          Quaternion.Euler(new Vector3(-90, -rotationDegrees, 0));
      tileSymbolView.gameObject.transform.localScale =
          new Vector3(1, -1, elevationStepHeight * height);
      tileSymbolView.gameObject.transform.localPosition =
          new Vector3(0, elevationStepHeight * elevation);
    }

    public void AddItem(ulong id, ExtrudedSymbolDescription symbolDescription) {
      foreach (var x in itemSymbolViewByItemId) {
        if (x.Item1 == id) {
          Asserts.Assert(false, "Item ID " + id + " already exists!");
        }
      }
      var itemSymbolView = instantiator.CreateSymbolView(clock, false, true, symbolDescription);
      itemSymbolViewByItemId.Add((id, itemSymbolView));
      UpdateItemPositions();
    }

    public void ClearItems() {
      foreach (var x in itemSymbolViewByItemId) {
        x.Item2.Destruct();
      }
      itemSymbolViewByItemId.Clear();
    }

    public void RemoveItem(ulong id) {
      for (int i = 0; i < itemSymbolViewByItemId.Count; i++) {
        if (itemSymbolViewByItemId[i].Item1 == id) {
          itemSymbolViewByItemId[i].Item2.Destruct();
          itemSymbolViewByItemId.RemoveAt(i);
          UpdateItemPositions();
          return;
        }
      }
      Asserts.Assert(false, "Item ID " + id + " doesnt exist!");
    }

    private void UpdateItemPositions() {
      float[] radiansForIndex = {
          0 * (float)Math.PI / 180,
          120 * (float)Math.PI / 180,
          240 * (float)Math.PI / 180,
          60 * (float)Math.PI / 180,
          180 * (float)Math.PI / 180,
          300 * (float)Math.PI / 180,
          30 * (float)Math.PI / 180,
          150 * (float)Math.PI / 180,
          270 * (float)Math.PI / 180,
          90 * (float)Math.PI / 180,
          210 * (float)Math.PI / 180,
          330 * (float)Math.PI / 180,
          // one can calculate the angles past this, but its probably noise at this point
      };
      
      for (int itemIndex = 0; itemIndex < itemSymbolViewByItemId.Count; itemIndex++) {
        var itemId = itemSymbolViewByItemId[itemIndex].Item1;
        var itemSymbolView = itemSymbolViewByItemId[itemIndex].Item2;

        float inscribeCircleRadius = 0.75f; // chosen cuz it looks about right
                                            // https://math.stackexchange.com/questions/666491/three-circles-within-a-larger-circle
        float itemRadius = (-3 + 2 * 1.732f) * inscribeCircleRadius;

        float itemCenterXFromTileCenter = 0;
        float itemCenterYFromTileCenter = 0;

        if (itemSymbolViewByItemId.Count == 1) {
          itemCenterXFromTileCenter = 0;
          itemCenterYFromTileCenter = 0;
        } else if (itemSymbolViewByItemId.Count == 2) {
          if (itemIndex == 0) {
            itemCenterXFromTileCenter = -itemRadius / 2;
            itemCenterYFromTileCenter = 0;
          } else {
            itemCenterXFromTileCenter = itemRadius / 2;
            itemCenterYFromTileCenter = 0;
          }
        } else {
          // 0.866 is cos(30)
          // I don't know why we need that / 2 there.
          float itemCenterDistanceToTileCenter = itemRadius / 0.866f / 2;
          itemCenterXFromTileCenter = itemCenterDistanceToTileCenter * (float)Math.Cos(radiansForIndex[itemIndex % radiansForIndex.Length]);
          itemCenterYFromTileCenter = itemCenterDistanceToTileCenter * (float)Math.Sin(radiansForIndex[itemIndex % radiansForIndex.Length]);
          // TODO: adjust upward if the unit is on the tile
          itemCenterYFromTileCenter += 0;
        }

        itemSymbolView.gameObject.transform.localPosition =
            new Vector3(
                itemCenterXFromTileCenter,
                .05f,
                itemCenterYFromTileCenter);
        itemSymbolView.gameObject.transform.localRotation =
            Quaternion.Euler(new Vector3(-90, 0f, 0));
        itemSymbolView.gameObject.transform.localScale =
          new Vector3(
              -1 * itemRadius,
              -1 * itemRadius,
              .1f);

        itemSymbolView.gameObject.transform.SetParent(transform, false);
      }
    }
    
    // public void SetItems(List<(ulong, ExtrudedSymbolDescription)> items) {
    //   foreach (var x in itemSymbolViewByItemId) {
    //     x.Item2.Destruct();
    //   }
    //   itemSymbolViewByItemId.Clear();
    //   for (int itemIndex = 0; itemIndex < items.Count; itemIndex++) {
    //     var itemId = items[itemIndex].Item1;
    //     var description = items[itemIndex].Item2;
    //     var itemSymbolView = instantiator.CreateSymbolView(clock, false, true, description);
    //
    //     float inscribeCircleRadius = 0.75f; // chosen cuz it looks about right
    //                                         // https://math.stackexchange.com/questions/666491/three-circles-within-a-larger-circle
    //     float itemRadius = (-3 + 2 * 1.732f) * inscribeCircleRadius;
    //
    //     float itemCenterXFromTileCenter = 0;
    //     float itemCenterYFromTileCenter = 0;
    //
    //     if (items.Count == 1) {
    //       itemCenterXFromTileCenter = 0;
    //       itemCenterYFromTileCenter = 0;
    //     } else if (items.Count == 2) {
    //       if (itemIndex == 0) {
    //         itemCenterXFromTileCenter = -itemRadius / 2;
    //         itemCenterYFromTileCenter = 0;
    //       } else {
    //         itemCenterXFromTileCenter = itemRadius / 2;
    //         itemCenterYFromTileCenter = 0;
    //       }
    //     } else {
    //       // 0.866 is cos(30)
    //       // I don't know why we need that / 2 there.
    //       float itemCenterDistanceToTileCenter = itemRadius / 0.866f / 2;
    //
    //       if (itemIndex == 0) {
    //         itemCenterXFromTileCenter = 0;
    //         itemCenterYFromTileCenter = itemCenterDistanceToTileCenter;
    //       } else if (itemIndex == 1) {
    //         // 0.866 is cos(30)
    //         itemCenterXFromTileCenter = 0.866f * itemCenterDistanceToTileCenter;
    //         // 0.5f is sin(30)
    //         itemCenterYFromTileCenter = -0.5f * itemCenterDistanceToTileCenter;
    //       } else if (itemIndex == 2) {
    //         // 0.866 is cos(30)
    //         itemCenterXFromTileCenter = -0.866f * itemCenterDistanceToTileCenter;
    //         // 0.5f is sin(30)
    //         itemCenterYFromTileCenter = -0.5f * itemCenterDistanceToTileCenter;
    //       }
    //
    //       // TODO: adjust upward if the unit is on the tile
    //       itemCenterYFromTileCenter += 0;
    //     }
    //
    //     itemSymbolView.gameObject.transform.localPosition =
    //         new Vector3(
    //             itemCenterXFromTileCenter,
    //             .05f,
    //             itemCenterYFromTileCenter);
    //     itemSymbolView.gameObject.transform.localRotation =
    //         Quaternion.Euler(new Vector3(-90, 0f, 0));
    //     itemSymbolView.gameObject.transform.localScale =
    //       new Vector3(
    //           -1 * itemRadius,
    //           -1 * itemRadius,
    //           .1f);
    //
    //     itemSymbolView.gameObject.transform.SetParent(transform, false);
    //
    //     itemSymbolViewByItemId.Add(itemId, itemSymbolView);
    //
    //     if (itemIndex == 4) {
    //       break;
    //     }
    //   }
    // }

    public void SetFrontColor(IVector4Animation frontColor) {
      this.topColor = frontColor;
      foreach (var tsv in tileSymbolViews) {
        tsv.SetFrontColor(frontColor);
      }
    }

    public void SetSidesColor(IVector4Animation sideColor) {
      this.sideColor = sideColor;
      foreach (var tsv in tileSymbolViews) {
        tsv.SetSidesColor(sideColor);
      }
    }
    
    public void SetFeature(ExtrudedSymbolDescription maybeFeature) {
      if (this.maybeFeature != null) {
        featureSymbolView.Destruct();
      }
      this.maybeFeature = maybeFeature;
      if (this.maybeFeature != null) {
        featureSymbolView = instantiator.CreateSymbolView(clock, false, true, this.maybeFeature);
        featureSymbolView.gameObject.transform.localPosition = new Vector3(0, .28f, .15f);
        featureSymbolView.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(180 + 50, 0f, 0f));
        featureSymbolView.gameObject.transform.localScale = new Vector3(-.8f, -.8f, .1f);
        featureSymbolView.gameObject.transform.SetParent(transform, false);
      }
    }

    public void SetOverlay(ExtrudedSymbolDescription maybeOverlay) {
      if (this.maybeOverlay != null) {
        overlaySymbolView.Destruct();
      }
      this.maybeOverlay = maybeOverlay;
      if (this.maybeOverlay != null) {
        overlaySymbolView = instantiator.CreateSymbolView(clock, false, true, maybeOverlay);
        overlaySymbolView.gameObject.transform.localPosition = new Vector3(0, .01f, 0);
        overlaySymbolView.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        overlaySymbolView.gameObject.transform.localScale = new Vector3(-1 * 0.707f, -1 * 0.707f, 1);
        overlaySymbolView.gameObject.transform.SetParent(transform, false);
      }
    }

    public void SetDepth(int depth) {
      while (tileSymbolViews.Count > depth) {
        var tsv = tileSymbolViews[tileSymbolViews.Count - 1];
        tileSymbolViews.RemoveAt(tileSymbolViews.Count - 1);
        tsv.Destruct();
      }
      var description =
          new ExtrudedSymbolDescription(
              RenderPriority.TILE,
              new SymbolDescription(
                  tileSymbolId,
                  topColor,
                  tileRotationDegrees,
                  tileScale,
                  tileOutlineMode,
                  tileOutlineColor),
              true,
              sideColor);
      while (tileSymbolViews.Count < depth) {
        var newIndex = tileSymbolViews.Count;
        SymbolView tileSymbolView = instantiator.CreateSymbolView(clock, true, true, description);
        SetTileOrPrismTransform(tileSymbolView, elevationStepHeight, tileRotationDegrees, -newIndex, 1);
        tileSymbolView.gameObject.transform.SetParent(transform, false);
        tileSymbolViews.Add(tileSymbolView);
      }
    }


    public void ShowRune(ExtrudedSymbolDescription runeSymbolDescription) {
      var symbolView = instantiator.CreateSymbolView(clock, false, true, runeSymbolDescription);
      symbolView.transform.localRotation = Quaternion.Euler(new Vector3(-50, 180, 0));
      symbolView.transform.localScale = new Vector3(1, 1, .1f);
      symbolView.transform.localPosition = new Vector3(0, 0.5f, -.2f);
      symbolView.transform.SetParent(gameObject.transform, false);
      symbolView.FadeInThenOut(100, 400);
      timer.ScheduleTimer(1000, () => {
        if (alive) {
          symbolView.Destruct();
        }
      });
    }

    public void FadeInThenOut(long inDurationMs, long outDurationMs) {
      List<SymbolView> allSymbolViews = new List<SymbolView>();
      allSymbolViews.AddRange(tileSymbolViews);
      allSymbolViews.Add(overlaySymbolView);
      allSymbolViews.Add(featureSymbolView);
      foreach (var thing in itemSymbolViewByItemId) {
        allSymbolViews.Add(thing.Item2);
      }
      foreach (var symbol in allSymbolViews) {
        symbol.FadeInThenOut(inDurationMs, outDurationMs);
      }
    }

    public long ShowPrism(
      ExtrudedSymbolDescription prismDescription,
      ExtrudedSymbolDescription prismOverlayDescription) {

      var prismGameObject = instantiator.CreateEmptyGameObject();
      prismGameObject.transform.SetParent(gameObject.transform, false);
      // We want to rotate the overall prism object because we want the
      // overlay symbol to be aligned with the camera but want the polygon
      // symbol to be aligned with the terrain tile.
      // However, we do animate the scale of this object.
      var scaleAnimator = ScaleAnimator.MakeOrGetFrom(clock, prismGameObject);
      var yScaleAnimation =
        new AddFloatAnimation(
          new ConstantFloatAnimation(.95f),
          new MultiplyFloatAnimation(
            new ConstantFloatAnimation(.05f),
            FloatAnimations.InThenOut(clock.GetTimeMs(), 100, 400, 1, 1, 0)));
      var scaleAnimation = new Vector3Animation(new ConstantFloatAnimation(.9f), yScaleAnimation, new ConstantFloatAnimation(.9f));
      scaleAnimator.Set(scaleAnimation);

      var polygonView =
        instantiator.CreateSymbolView(
          clock,
          false,
          false,
          prismDescription);
      SetTileOrPrismTransform(polygonView, elevationStepHeight, tileRotationDegrees, 0, 3);
      polygonView.transform.SetParent(prismGameObject.transform, false);
      polygonView.FadeInThenOut(100, 400);
      ScheduleSymbolViewDestruction(polygonView);

      var overlayView =
        instantiator.CreateSymbolView(
          clock,
          false,
          false,
          prismOverlayDescription);

      float overlayThickness = .35f * elevationStepHeight;
      // No idea why we need the -90. It has to do with
      // unity's infuriating mishandling of .obj file imports.
      overlayView.gameObject.transform.localRotation =
          Quaternion.Euler(new Vector3(-90, 0, 0));
      overlayView.gameObject.transform.localScale =
          new Vector3(1 * .8f, -1 * .8f, overlayThickness);
      overlayView.gameObject.transform.localPosition =
          new Vector3(0, elevationStepHeight * 3f + overlayThickness);

      overlayView.transform.SetParent(prismGameObject.transform, false);
      overlayView.FadeInThenOut(100, 400);
      ScheduleSymbolViewDestruction(overlayView);

      return clock.GetTimeMs() + 500;
    }

    private void ScheduleSymbolViewDestruction(SymbolView symbolView) {
      int prismOverlayTimerId =
        timer.ScheduleTimer(1000, () => {
          for (int i = 0; i < transientPrismSymbolsAndTimerIds.Count; i++) {
            if (transientPrismSymbolsAndTimerIds[i].Key == symbolView) {
              transientPrismSymbolsAndTimerIds.RemoveAt(i);
              symbolView.Destruct();
              return;
            }
          }
          Asserts.Assert(false, "Couldnt find!");
        });
      transientPrismSymbolsAndTimerIds.Add(new KeyValuePair<SymbolView, int>(symbolView, prismOverlayTimerId));
    }

    public void Start() {
      if (!initialized) {
        throw new Exception("TileView component not initialized!");
      }
    }
  }
}