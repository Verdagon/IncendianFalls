using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ITerrainTileComponentMutBunch {
  public readonly Root root;
  public readonly int id;
  public ITerrainTileComponentMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public ITerrainTileComponentMutBunchIncarnation incarnation { get { return root.GetITerrainTileComponentMutBunchIncarnation(id); } }
  public void AddObserver(IITerrainTileComponentMutBunchEffectObserver observer) {
    root.AddITerrainTileComponentMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IITerrainTileComponentMutBunchEffectObserver observer) {
    root.RemoveITerrainTileComponentMutBunchObserver(id, observer);
  }
  public void Delete() {
    root.EffectITerrainTileComponentMutBunchDelete(id);
  }
  public static ITerrainTileComponentMutBunch Null = new ITerrainTileComponentMutBunch(null, 0);
  public bool Exists() { return root != null && root.ITerrainTileComponentMutBunchExists(id); }
  public bool NullableIs(ITerrainTileComponentMutBunch that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.SimplePresenceTriggerTTCMutSetExists(membersSimplePresenceTriggerTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersSimplePresenceTriggerTTCMutSet");
    }

    if (!root.ItemTTCMutSetExists(membersItemTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersItemTTCMutSet");
    }

    if (!root.WarperTTCMutSetExists(membersWarperTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersWarperTTCMutSet");
    }

    if (!root.TimeAnchorTTCMutSetExists(membersTimeAnchorTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersTimeAnchorTTCMutSet");
    }

    if (!root.MarkerTTCMutSetExists(membersMarkerTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersMarkerTTCMutSet");
    }

    if (!root.LevelLinkTTCMutSetExists(membersLevelLinkTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersLevelLinkTTCMutSet");
    }

    if (!root.MudTTCMutSetExists(membersMudTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersMudTTCMutSet");
    }

    if (!root.DirtTTCMutSetExists(membersDirtTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersDirtTTCMutSet");
    }

    if (!root.ObsidianTTCMutSetExists(membersObsidianTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersObsidianTTCMutSet");
    }

    if (!root.DownStairsTTCMutSetExists(membersDownStairsTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersDownStairsTTCMutSet");
    }

    if (!root.UpStairsTTCMutSetExists(membersUpStairsTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersUpStairsTTCMutSet");
    }

    if (!root.WallTTCMutSetExists(membersWallTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersWallTTCMutSet");
    }

    if (!root.BloodTTCMutSetExists(membersBloodTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersBloodTTCMutSet");
    }

    if (!root.RocksTTCMutSetExists(membersRocksTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersRocksTTCMutSet");
    }

    if (!root.TreeTTCMutSetExists(membersTreeTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersTreeTTCMutSet");
    }

    if (!root.WaterTTCMutSetExists(membersWaterTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersWaterTTCMutSet");
    }

    if (!root.FloorTTCMutSetExists(membersFloorTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersFloorTTCMutSet");
    }

    if (!root.CaveWallTTCMutSetExists(membersCaveWallTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersCaveWallTTCMutSet");
    }

    if (!root.CaveTTCMutSetExists(membersCaveTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersCaveTTCMutSet");
    }

    if (!root.FallsTTCMutSetExists(membersFallsTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersFallsTTCMutSet");
    }

    if (!root.MagmaTTCMutSetExists(membersMagmaTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersMagmaTTCMutSet");
    }

    if (!root.CliffTTCMutSetExists(membersCliffTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersCliffTTCMutSet");
    }

    if (!root.RavaNestTTCMutSetExists(membersRavaNestTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersRavaNestTTCMutSet");
    }

    if (!root.CliffLandingTTCMutSetExists(membersCliffLandingTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersCliffLandingTTCMutSet");
    }

    if (!root.StoneTTCMutSetExists(membersStoneTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersStoneTTCMutSet");
    }

    if (!root.GrassTTCMutSetExists(membersGrassTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersGrassTTCMutSet");
    }

    if (!root.IncendianFallsLevelLinkerTTCMutSetExists(membersIncendianFallsLevelLinkerTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersIncendianFallsLevelLinkerTTCMutSet");
    }

    if (!root.EmberDeepLevelLinkerTTCMutSetExists(membersEmberDeepLevelLinkerTTCMutSet.id)) {
      violations.Add("Null constraint violated! ITerrainTileComponentMutBunch#" + id + ".membersEmberDeepLevelLinkerTTCMutSet");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.SimplePresenceTriggerTTCMutSetExists(membersSimplePresenceTriggerTTCMutSet.id)) {
      membersSimplePresenceTriggerTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.ItemTTCMutSetExists(membersItemTTCMutSet.id)) {
      membersItemTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.WarperTTCMutSetExists(membersWarperTTCMutSet.id)) {
      membersWarperTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.TimeAnchorTTCMutSetExists(membersTimeAnchorTTCMutSet.id)) {
      membersTimeAnchorTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.MarkerTTCMutSetExists(membersMarkerTTCMutSet.id)) {
      membersMarkerTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.LevelLinkTTCMutSetExists(membersLevelLinkTTCMutSet.id)) {
      membersLevelLinkTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.MudTTCMutSetExists(membersMudTTCMutSet.id)) {
      membersMudTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.DirtTTCMutSetExists(membersDirtTTCMutSet.id)) {
      membersDirtTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.ObsidianTTCMutSetExists(membersObsidianTTCMutSet.id)) {
      membersObsidianTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.DownStairsTTCMutSetExists(membersDownStairsTTCMutSet.id)) {
      membersDownStairsTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.UpStairsTTCMutSetExists(membersUpStairsTTCMutSet.id)) {
      membersUpStairsTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.WallTTCMutSetExists(membersWallTTCMutSet.id)) {
      membersWallTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.BloodTTCMutSetExists(membersBloodTTCMutSet.id)) {
      membersBloodTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.RocksTTCMutSetExists(membersRocksTTCMutSet.id)) {
      membersRocksTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.TreeTTCMutSetExists(membersTreeTTCMutSet.id)) {
      membersTreeTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.WaterTTCMutSetExists(membersWaterTTCMutSet.id)) {
      membersWaterTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.FloorTTCMutSetExists(membersFloorTTCMutSet.id)) {
      membersFloorTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.CaveWallTTCMutSetExists(membersCaveWallTTCMutSet.id)) {
      membersCaveWallTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.CaveTTCMutSetExists(membersCaveTTCMutSet.id)) {
      membersCaveTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.FallsTTCMutSetExists(membersFallsTTCMutSet.id)) {
      membersFallsTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.MagmaTTCMutSetExists(membersMagmaTTCMutSet.id)) {
      membersMagmaTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.CliffTTCMutSetExists(membersCliffTTCMutSet.id)) {
      membersCliffTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.RavaNestTTCMutSetExists(membersRavaNestTTCMutSet.id)) {
      membersRavaNestTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.CliffLandingTTCMutSetExists(membersCliffLandingTTCMutSet.id)) {
      membersCliffLandingTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.StoneTTCMutSetExists(membersStoneTTCMutSet.id)) {
      membersStoneTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.GrassTTCMutSetExists(membersGrassTTCMutSet.id)) {
      membersGrassTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.IncendianFallsLevelLinkerTTCMutSetExists(membersIncendianFallsLevelLinkerTTCMutSet.id)) {
      membersIncendianFallsLevelLinkerTTCMutSet.FindReachableObjects(foundIds);
    }
    if (root.EmberDeepLevelLinkerTTCMutSetExists(membersEmberDeepLevelLinkerTTCMutSet.id)) {
      membersEmberDeepLevelLinkerTTCMutSet.FindReachableObjects(foundIds);
    }
  }
  public bool Is(ITerrainTileComponentMutBunch that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public SimplePresenceTriggerTTCMutSet membersSimplePresenceTriggerTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersSimplePresenceTriggerTTCMutSet of null!");
      }
      return new SimplePresenceTriggerTTCMutSet(root, incarnation.membersSimplePresenceTriggerTTCMutSet);
    }
                       }
  public ItemTTCMutSet membersItemTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersItemTTCMutSet of null!");
      }
      return new ItemTTCMutSet(root, incarnation.membersItemTTCMutSet);
    }
                       }
  public WarperTTCMutSet membersWarperTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersWarperTTCMutSet of null!");
      }
      return new WarperTTCMutSet(root, incarnation.membersWarperTTCMutSet);
    }
                       }
  public TimeAnchorTTCMutSet membersTimeAnchorTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersTimeAnchorTTCMutSet of null!");
      }
      return new TimeAnchorTTCMutSet(root, incarnation.membersTimeAnchorTTCMutSet);
    }
                       }
  public MarkerTTCMutSet membersMarkerTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersMarkerTTCMutSet of null!");
      }
      return new MarkerTTCMutSet(root, incarnation.membersMarkerTTCMutSet);
    }
                       }
  public LevelLinkTTCMutSet membersLevelLinkTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersLevelLinkTTCMutSet of null!");
      }
      return new LevelLinkTTCMutSet(root, incarnation.membersLevelLinkTTCMutSet);
    }
                       }
  public MudTTCMutSet membersMudTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersMudTTCMutSet of null!");
      }
      return new MudTTCMutSet(root, incarnation.membersMudTTCMutSet);
    }
                       }
  public DirtTTCMutSet membersDirtTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersDirtTTCMutSet of null!");
      }
      return new DirtTTCMutSet(root, incarnation.membersDirtTTCMutSet);
    }
                       }
  public ObsidianTTCMutSet membersObsidianTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersObsidianTTCMutSet of null!");
      }
      return new ObsidianTTCMutSet(root, incarnation.membersObsidianTTCMutSet);
    }
                       }
  public DownStairsTTCMutSet membersDownStairsTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersDownStairsTTCMutSet of null!");
      }
      return new DownStairsTTCMutSet(root, incarnation.membersDownStairsTTCMutSet);
    }
                       }
  public UpStairsTTCMutSet membersUpStairsTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersUpStairsTTCMutSet of null!");
      }
      return new UpStairsTTCMutSet(root, incarnation.membersUpStairsTTCMutSet);
    }
                       }
  public WallTTCMutSet membersWallTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersWallTTCMutSet of null!");
      }
      return new WallTTCMutSet(root, incarnation.membersWallTTCMutSet);
    }
                       }
  public BloodTTCMutSet membersBloodTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersBloodTTCMutSet of null!");
      }
      return new BloodTTCMutSet(root, incarnation.membersBloodTTCMutSet);
    }
                       }
  public RocksTTCMutSet membersRocksTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersRocksTTCMutSet of null!");
      }
      return new RocksTTCMutSet(root, incarnation.membersRocksTTCMutSet);
    }
                       }
  public TreeTTCMutSet membersTreeTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersTreeTTCMutSet of null!");
      }
      return new TreeTTCMutSet(root, incarnation.membersTreeTTCMutSet);
    }
                       }
  public WaterTTCMutSet membersWaterTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersWaterTTCMutSet of null!");
      }
      return new WaterTTCMutSet(root, incarnation.membersWaterTTCMutSet);
    }
                       }
  public FloorTTCMutSet membersFloorTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersFloorTTCMutSet of null!");
      }
      return new FloorTTCMutSet(root, incarnation.membersFloorTTCMutSet);
    }
                       }
  public CaveWallTTCMutSet membersCaveWallTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersCaveWallTTCMutSet of null!");
      }
      return new CaveWallTTCMutSet(root, incarnation.membersCaveWallTTCMutSet);
    }
                       }
  public CaveTTCMutSet membersCaveTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersCaveTTCMutSet of null!");
      }
      return new CaveTTCMutSet(root, incarnation.membersCaveTTCMutSet);
    }
                       }
  public FallsTTCMutSet membersFallsTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersFallsTTCMutSet of null!");
      }
      return new FallsTTCMutSet(root, incarnation.membersFallsTTCMutSet);
    }
                       }
  public MagmaTTCMutSet membersMagmaTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersMagmaTTCMutSet of null!");
      }
      return new MagmaTTCMutSet(root, incarnation.membersMagmaTTCMutSet);
    }
                       }
  public CliffTTCMutSet membersCliffTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersCliffTTCMutSet of null!");
      }
      return new CliffTTCMutSet(root, incarnation.membersCliffTTCMutSet);
    }
                       }
  public RavaNestTTCMutSet membersRavaNestTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersRavaNestTTCMutSet of null!");
      }
      return new RavaNestTTCMutSet(root, incarnation.membersRavaNestTTCMutSet);
    }
                       }
  public CliffLandingTTCMutSet membersCliffLandingTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersCliffLandingTTCMutSet of null!");
      }
      return new CliffLandingTTCMutSet(root, incarnation.membersCliffLandingTTCMutSet);
    }
                       }
  public StoneTTCMutSet membersStoneTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersStoneTTCMutSet of null!");
      }
      return new StoneTTCMutSet(root, incarnation.membersStoneTTCMutSet);
    }
                       }
  public GrassTTCMutSet membersGrassTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersGrassTTCMutSet of null!");
      }
      return new GrassTTCMutSet(root, incarnation.membersGrassTTCMutSet);
    }
                       }
  public IncendianFallsLevelLinkerTTCMutSet membersIncendianFallsLevelLinkerTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersIncendianFallsLevelLinkerTTCMutSet of null!");
      }
      return new IncendianFallsLevelLinkerTTCMutSet(root, incarnation.membersIncendianFallsLevelLinkerTTCMutSet);
    }
                       }
  public EmberDeepLevelLinkerTTCMutSet membersEmberDeepLevelLinkerTTCMutSet {

    get {
      if (root == null) {
        throw new Exception("Tried to get member membersEmberDeepLevelLinkerTTCMutSet of null!");
      }
      return new EmberDeepLevelLinkerTTCMutSet(root, incarnation.membersEmberDeepLevelLinkerTTCMutSet);
    }
                       }

  public static ITerrainTileComponentMutBunch New(Root root) {
    return root.EffectITerrainTileComponentMutBunchCreate(
      root.EffectSimplePresenceTriggerTTCMutSetCreate()
,
      root.EffectItemTTCMutSetCreate()
,
      root.EffectWarperTTCMutSetCreate()
,
      root.EffectTimeAnchorTTCMutSetCreate()
,
      root.EffectMarkerTTCMutSetCreate()
,
      root.EffectLevelLinkTTCMutSetCreate()
,
      root.EffectMudTTCMutSetCreate()
,
      root.EffectDirtTTCMutSetCreate()
,
      root.EffectObsidianTTCMutSetCreate()
,
      root.EffectDownStairsTTCMutSetCreate()
,
      root.EffectUpStairsTTCMutSetCreate()
,
      root.EffectWallTTCMutSetCreate()
,
      root.EffectBloodTTCMutSetCreate()
,
      root.EffectRocksTTCMutSetCreate()
,
      root.EffectTreeTTCMutSetCreate()
,
      root.EffectWaterTTCMutSetCreate()
,
      root.EffectFloorTTCMutSetCreate()
,
      root.EffectCaveWallTTCMutSetCreate()
,
      root.EffectCaveTTCMutSetCreate()
,
      root.EffectFallsTTCMutSetCreate()
,
      root.EffectMagmaTTCMutSetCreate()
,
      root.EffectCliffTTCMutSetCreate()
,
      root.EffectRavaNestTTCMutSetCreate()
,
      root.EffectCliffLandingTTCMutSetCreate()
,
      root.EffectStoneTTCMutSetCreate()
,
      root.EffectGrassTTCMutSetCreate()
,
      root.EffectIncendianFallsLevelLinkerTTCMutSetCreate()
,
      root.EffectEmberDeepLevelLinkerTTCMutSetCreate()
        );
  }
  public void Add(ITerrainTileComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.SimplePresenceTriggerTTCExists(elementI.id)) {
      this.membersSimplePresenceTriggerTTCMutSet.Add(root.GetSimplePresenceTriggerTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ItemTTCExists(elementI.id)) {
      this.membersItemTTCMutSet.Add(root.GetItemTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WarperTTCExists(elementI.id)) {
      this.membersWarperTTCMutSet.Add(root.GetWarperTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TimeAnchorTTCExists(elementI.id)) {
      this.membersTimeAnchorTTCMutSet.Add(root.GetTimeAnchorTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MarkerTTCExists(elementI.id)) {
      this.membersMarkerTTCMutSet.Add(root.GetMarkerTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.LevelLinkTTCExists(elementI.id)) {
      this.membersLevelLinkTTCMutSet.Add(root.GetLevelLinkTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MudTTCExists(elementI.id)) {
      this.membersMudTTCMutSet.Add(root.GetMudTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DirtTTCExists(elementI.id)) {
      this.membersDirtTTCMutSet.Add(root.GetDirtTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ObsidianTTCExists(elementI.id)) {
      this.membersObsidianTTCMutSet.Add(root.GetObsidianTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DownStairsTTCExists(elementI.id)) {
      this.membersDownStairsTTCMutSet.Add(root.GetDownStairsTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.UpStairsTTCExists(elementI.id)) {
      this.membersUpStairsTTCMutSet.Add(root.GetUpStairsTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WallTTCExists(elementI.id)) {
      this.membersWallTTCMutSet.Add(root.GetWallTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BloodTTCExists(elementI.id)) {
      this.membersBloodTTCMutSet.Add(root.GetBloodTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.RocksTTCExists(elementI.id)) {
      this.membersRocksTTCMutSet.Add(root.GetRocksTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TreeTTCExists(elementI.id)) {
      this.membersTreeTTCMutSet.Add(root.GetTreeTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WaterTTCExists(elementI.id)) {
      this.membersWaterTTCMutSet.Add(root.GetWaterTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.FloorTTCExists(elementI.id)) {
      this.membersFloorTTCMutSet.Add(root.GetFloorTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CaveWallTTCExists(elementI.id)) {
      this.membersCaveWallTTCMutSet.Add(root.GetCaveWallTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CaveTTCExists(elementI.id)) {
      this.membersCaveTTCMutSet.Add(root.GetCaveTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.FallsTTCExists(elementI.id)) {
      this.membersFallsTTCMutSet.Add(root.GetFallsTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MagmaTTCExists(elementI.id)) {
      this.membersMagmaTTCMutSet.Add(root.GetMagmaTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CliffTTCExists(elementI.id)) {
      this.membersCliffTTCMutSet.Add(root.GetCliffTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.RavaNestTTCExists(elementI.id)) {
      this.membersRavaNestTTCMutSet.Add(root.GetRavaNestTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CliffLandingTTCExists(elementI.id)) {
      this.membersCliffLandingTTCMutSet.Add(root.GetCliffLandingTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.StoneTTCExists(elementI.id)) {
      this.membersStoneTTCMutSet.Add(root.GetStoneTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GrassTTCExists(elementI.id)) {
      this.membersGrassTTCMutSet.Add(root.GetGrassTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.IncendianFallsLevelLinkerTTCExists(elementI.id)) {
      this.membersIncendianFallsLevelLinkerTTCMutSet.Add(root.GetIncendianFallsLevelLinkerTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.EmberDeepLevelLinkerTTCExists(elementI.id)) {
      this.membersEmberDeepLevelLinkerTTCMutSet.Add(root.GetEmberDeepLevelLinkerTTC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Remove(ITerrainTileComponent elementI) {

    // Can optimize, check the type of element directly somehow
    if (root.SimplePresenceTriggerTTCExists(elementI.id)) {
      this.membersSimplePresenceTriggerTTCMutSet.Remove(root.GetSimplePresenceTriggerTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ItemTTCExists(elementI.id)) {
      this.membersItemTTCMutSet.Remove(root.GetItemTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WarperTTCExists(elementI.id)) {
      this.membersWarperTTCMutSet.Remove(root.GetWarperTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TimeAnchorTTCExists(elementI.id)) {
      this.membersTimeAnchorTTCMutSet.Remove(root.GetTimeAnchorTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MarkerTTCExists(elementI.id)) {
      this.membersMarkerTTCMutSet.Remove(root.GetMarkerTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.LevelLinkTTCExists(elementI.id)) {
      this.membersLevelLinkTTCMutSet.Remove(root.GetLevelLinkTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MudTTCExists(elementI.id)) {
      this.membersMudTTCMutSet.Remove(root.GetMudTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DirtTTCExists(elementI.id)) {
      this.membersDirtTTCMutSet.Remove(root.GetDirtTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.ObsidianTTCExists(elementI.id)) {
      this.membersObsidianTTCMutSet.Remove(root.GetObsidianTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.DownStairsTTCExists(elementI.id)) {
      this.membersDownStairsTTCMutSet.Remove(root.GetDownStairsTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.UpStairsTTCExists(elementI.id)) {
      this.membersUpStairsTTCMutSet.Remove(root.GetUpStairsTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WallTTCExists(elementI.id)) {
      this.membersWallTTCMutSet.Remove(root.GetWallTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.BloodTTCExists(elementI.id)) {
      this.membersBloodTTCMutSet.Remove(root.GetBloodTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.RocksTTCExists(elementI.id)) {
      this.membersRocksTTCMutSet.Remove(root.GetRocksTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.TreeTTCExists(elementI.id)) {
      this.membersTreeTTCMutSet.Remove(root.GetTreeTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.WaterTTCExists(elementI.id)) {
      this.membersWaterTTCMutSet.Remove(root.GetWaterTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.FloorTTCExists(elementI.id)) {
      this.membersFloorTTCMutSet.Remove(root.GetFloorTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CaveWallTTCExists(elementI.id)) {
      this.membersCaveWallTTCMutSet.Remove(root.GetCaveWallTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CaveTTCExists(elementI.id)) {
      this.membersCaveTTCMutSet.Remove(root.GetCaveTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.FallsTTCExists(elementI.id)) {
      this.membersFallsTTCMutSet.Remove(root.GetFallsTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.MagmaTTCExists(elementI.id)) {
      this.membersMagmaTTCMutSet.Remove(root.GetMagmaTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CliffTTCExists(elementI.id)) {
      this.membersCliffTTCMutSet.Remove(root.GetCliffTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.RavaNestTTCExists(elementI.id)) {
      this.membersRavaNestTTCMutSet.Remove(root.GetRavaNestTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.CliffLandingTTCExists(elementI.id)) {
      this.membersCliffLandingTTCMutSet.Remove(root.GetCliffLandingTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.StoneTTCExists(elementI.id)) {
      this.membersStoneTTCMutSet.Remove(root.GetStoneTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.GrassTTCExists(elementI.id)) {
      this.membersGrassTTCMutSet.Remove(root.GetGrassTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.IncendianFallsLevelLinkerTTCExists(elementI.id)) {
      this.membersIncendianFallsLevelLinkerTTCMutSet.Remove(root.GetIncendianFallsLevelLinkerTTC(elementI.id));
      return;
    }

    // Can optimize, check the type of element directly somehow
    if (root.EmberDeepLevelLinkerTTCExists(elementI.id)) {
      this.membersEmberDeepLevelLinkerTTCMutSet.Remove(root.GetEmberDeepLevelLinkerTTC(elementI.id));
      return;
    }
    throw new Exception("Unknown type " + elementI);
  }
  public void Clear() {
    this.membersSimplePresenceTriggerTTCMutSet.Clear();
    this.membersItemTTCMutSet.Clear();
    this.membersWarperTTCMutSet.Clear();
    this.membersTimeAnchorTTCMutSet.Clear();
    this.membersMarkerTTCMutSet.Clear();
    this.membersLevelLinkTTCMutSet.Clear();
    this.membersMudTTCMutSet.Clear();
    this.membersDirtTTCMutSet.Clear();
    this.membersObsidianTTCMutSet.Clear();
    this.membersDownStairsTTCMutSet.Clear();
    this.membersUpStairsTTCMutSet.Clear();
    this.membersWallTTCMutSet.Clear();
    this.membersBloodTTCMutSet.Clear();
    this.membersRocksTTCMutSet.Clear();
    this.membersTreeTTCMutSet.Clear();
    this.membersWaterTTCMutSet.Clear();
    this.membersFloorTTCMutSet.Clear();
    this.membersCaveWallTTCMutSet.Clear();
    this.membersCaveTTCMutSet.Clear();
    this.membersFallsTTCMutSet.Clear();
    this.membersMagmaTTCMutSet.Clear();
    this.membersCliffTTCMutSet.Clear();
    this.membersRavaNestTTCMutSet.Clear();
    this.membersCliffLandingTTCMutSet.Clear();
    this.membersStoneTTCMutSet.Clear();
    this.membersGrassTTCMutSet.Clear();
    this.membersIncendianFallsLevelLinkerTTCMutSet.Clear();
    this.membersEmberDeepLevelLinkerTTCMutSet.Clear();
  }
  public int Count {
    get {
      return
        this.membersSimplePresenceTriggerTTCMutSet.Count +
        this.membersItemTTCMutSet.Count +
        this.membersWarperTTCMutSet.Count +
        this.membersTimeAnchorTTCMutSet.Count +
        this.membersMarkerTTCMutSet.Count +
        this.membersLevelLinkTTCMutSet.Count +
        this.membersMudTTCMutSet.Count +
        this.membersDirtTTCMutSet.Count +
        this.membersObsidianTTCMutSet.Count +
        this.membersDownStairsTTCMutSet.Count +
        this.membersUpStairsTTCMutSet.Count +
        this.membersWallTTCMutSet.Count +
        this.membersBloodTTCMutSet.Count +
        this.membersRocksTTCMutSet.Count +
        this.membersTreeTTCMutSet.Count +
        this.membersWaterTTCMutSet.Count +
        this.membersFloorTTCMutSet.Count +
        this.membersCaveWallTTCMutSet.Count +
        this.membersCaveTTCMutSet.Count +
        this.membersFallsTTCMutSet.Count +
        this.membersMagmaTTCMutSet.Count +
        this.membersCliffTTCMutSet.Count +
        this.membersRavaNestTTCMutSet.Count +
        this.membersCliffLandingTTCMutSet.Count +
        this.membersStoneTTCMutSet.Count +
        this.membersGrassTTCMutSet.Count +
        this.membersIncendianFallsLevelLinkerTTCMutSet.Count +
        this.membersEmberDeepLevelLinkerTTCMutSet.Count
        ;
    }
  }
  public ITerrainTileComponent GetArbitrary() {
    foreach (var element in this) {
      return element;
    }
    throw new Exception("Can't get element from empty bunch!");
  }

  public void Destruct() {
    var tempMembersSimplePresenceTriggerTTCMutSet = this.membersSimplePresenceTriggerTTCMutSet;
    var tempMembersItemTTCMutSet = this.membersItemTTCMutSet;
    var tempMembersWarperTTCMutSet = this.membersWarperTTCMutSet;
    var tempMembersTimeAnchorTTCMutSet = this.membersTimeAnchorTTCMutSet;
    var tempMembersMarkerTTCMutSet = this.membersMarkerTTCMutSet;
    var tempMembersLevelLinkTTCMutSet = this.membersLevelLinkTTCMutSet;
    var tempMembersMudTTCMutSet = this.membersMudTTCMutSet;
    var tempMembersDirtTTCMutSet = this.membersDirtTTCMutSet;
    var tempMembersObsidianTTCMutSet = this.membersObsidianTTCMutSet;
    var tempMembersDownStairsTTCMutSet = this.membersDownStairsTTCMutSet;
    var tempMembersUpStairsTTCMutSet = this.membersUpStairsTTCMutSet;
    var tempMembersWallTTCMutSet = this.membersWallTTCMutSet;
    var tempMembersBloodTTCMutSet = this.membersBloodTTCMutSet;
    var tempMembersRocksTTCMutSet = this.membersRocksTTCMutSet;
    var tempMembersTreeTTCMutSet = this.membersTreeTTCMutSet;
    var tempMembersWaterTTCMutSet = this.membersWaterTTCMutSet;
    var tempMembersFloorTTCMutSet = this.membersFloorTTCMutSet;
    var tempMembersCaveWallTTCMutSet = this.membersCaveWallTTCMutSet;
    var tempMembersCaveTTCMutSet = this.membersCaveTTCMutSet;
    var tempMembersFallsTTCMutSet = this.membersFallsTTCMutSet;
    var tempMembersMagmaTTCMutSet = this.membersMagmaTTCMutSet;
    var tempMembersCliffTTCMutSet = this.membersCliffTTCMutSet;
    var tempMembersRavaNestTTCMutSet = this.membersRavaNestTTCMutSet;
    var tempMembersCliffLandingTTCMutSet = this.membersCliffLandingTTCMutSet;
    var tempMembersStoneTTCMutSet = this.membersStoneTTCMutSet;
    var tempMembersGrassTTCMutSet = this.membersGrassTTCMutSet;
    var tempMembersIncendianFallsLevelLinkerTTCMutSet = this.membersIncendianFallsLevelLinkerTTCMutSet;
    var tempMembersEmberDeepLevelLinkerTTCMutSet = this.membersEmberDeepLevelLinkerTTCMutSet;

    this.Delete();
    tempMembersSimplePresenceTriggerTTCMutSet.Destruct();
    tempMembersItemTTCMutSet.Destruct();
    tempMembersWarperTTCMutSet.Destruct();
    tempMembersTimeAnchorTTCMutSet.Destruct();
    tempMembersMarkerTTCMutSet.Destruct();
    tempMembersLevelLinkTTCMutSet.Destruct();
    tempMembersMudTTCMutSet.Destruct();
    tempMembersDirtTTCMutSet.Destruct();
    tempMembersObsidianTTCMutSet.Destruct();
    tempMembersDownStairsTTCMutSet.Destruct();
    tempMembersUpStairsTTCMutSet.Destruct();
    tempMembersWallTTCMutSet.Destruct();
    tempMembersBloodTTCMutSet.Destruct();
    tempMembersRocksTTCMutSet.Destruct();
    tempMembersTreeTTCMutSet.Destruct();
    tempMembersWaterTTCMutSet.Destruct();
    tempMembersFloorTTCMutSet.Destruct();
    tempMembersCaveWallTTCMutSet.Destruct();
    tempMembersCaveTTCMutSet.Destruct();
    tempMembersFallsTTCMutSet.Destruct();
    tempMembersMagmaTTCMutSet.Destruct();
    tempMembersCliffTTCMutSet.Destruct();
    tempMembersRavaNestTTCMutSet.Destruct();
    tempMembersCliffLandingTTCMutSet.Destruct();
    tempMembersStoneTTCMutSet.Destruct();
    tempMembersGrassTTCMutSet.Destruct();
    tempMembersIncendianFallsLevelLinkerTTCMutSet.Destruct();
    tempMembersEmberDeepLevelLinkerTTCMutSet.Destruct();
  }
  public IEnumerator<ITerrainTileComponent> GetEnumerator() {
    foreach (var element in this.membersSimplePresenceTriggerTTCMutSet) {
      yield return new SimplePresenceTriggerTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersItemTTCMutSet) {
      yield return new ItemTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersWarperTTCMutSet) {
      yield return new WarperTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersTimeAnchorTTCMutSet) {
      yield return new TimeAnchorTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersMarkerTTCMutSet) {
      yield return new MarkerTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersLevelLinkTTCMutSet) {
      yield return new LevelLinkTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersMudTTCMutSet) {
      yield return new MudTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersDirtTTCMutSet) {
      yield return new DirtTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersObsidianTTCMutSet) {
      yield return new ObsidianTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersDownStairsTTCMutSet) {
      yield return new DownStairsTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersUpStairsTTCMutSet) {
      yield return new UpStairsTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersWallTTCMutSet) {
      yield return new WallTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersBloodTTCMutSet) {
      yield return new BloodTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersRocksTTCMutSet) {
      yield return new RocksTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersTreeTTCMutSet) {
      yield return new TreeTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersWaterTTCMutSet) {
      yield return new WaterTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersFloorTTCMutSet) {
      yield return new FloorTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersCaveWallTTCMutSet) {
      yield return new CaveWallTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersCaveTTCMutSet) {
      yield return new CaveTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersFallsTTCMutSet) {
      yield return new FallsTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersMagmaTTCMutSet) {
      yield return new MagmaTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersCliffTTCMutSet) {
      yield return new CliffTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersRavaNestTTCMutSet) {
      yield return new RavaNestTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersCliffLandingTTCMutSet) {
      yield return new CliffLandingTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersStoneTTCMutSet) {
      yield return new StoneTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersGrassTTCMutSet) {
      yield return new GrassTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersIncendianFallsLevelLinkerTTCMutSet) {
      yield return new IncendianFallsLevelLinkerTTCAsITerrainTileComponent(element);
    }
    foreach (var element in this.membersEmberDeepLevelLinkerTTCMutSet) {
      yield return new EmberDeepLevelLinkerTTCAsITerrainTileComponent(element);
    }
  }
    public List<SimplePresenceTriggerTTC> GetAllSimplePresenceTriggerTTC() {
      var result = new List<SimplePresenceTriggerTTC>();
      foreach (var thing in this.membersSimplePresenceTriggerTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<SimplePresenceTriggerTTC> ClearAllSimplePresenceTriggerTTC() {
      var result = new List<SimplePresenceTriggerTTC>();
      this.membersSimplePresenceTriggerTTCMutSet.Clear();
      return result;
    }
    public SimplePresenceTriggerTTC GetOnlySimplePresenceTriggerTTCOrNull() {
      var result = GetAllSimplePresenceTriggerTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return SimplePresenceTriggerTTC.Null;
      }
    }
    public List<ItemTTC> GetAllItemTTC() {
      var result = new List<ItemTTC>();
      foreach (var thing in this.membersItemTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<ItemTTC> ClearAllItemTTC() {
      var result = new List<ItemTTC>();
      this.membersItemTTCMutSet.Clear();
      return result;
    }
    public ItemTTC GetOnlyItemTTCOrNull() {
      var result = GetAllItemTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return ItemTTC.Null;
      }
    }
    public List<WarperTTC> GetAllWarperTTC() {
      var result = new List<WarperTTC>();
      foreach (var thing in this.membersWarperTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<WarperTTC> ClearAllWarperTTC() {
      var result = new List<WarperTTC>();
      this.membersWarperTTCMutSet.Clear();
      return result;
    }
    public WarperTTC GetOnlyWarperTTCOrNull() {
      var result = GetAllWarperTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return WarperTTC.Null;
      }
    }
    public List<TimeAnchorTTC> GetAllTimeAnchorTTC() {
      var result = new List<TimeAnchorTTC>();
      foreach (var thing in this.membersTimeAnchorTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<TimeAnchorTTC> ClearAllTimeAnchorTTC() {
      var result = new List<TimeAnchorTTC>();
      this.membersTimeAnchorTTCMutSet.Clear();
      return result;
    }
    public TimeAnchorTTC GetOnlyTimeAnchorTTCOrNull() {
      var result = GetAllTimeAnchorTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return TimeAnchorTTC.Null;
      }
    }
    public List<MarkerTTC> GetAllMarkerTTC() {
      var result = new List<MarkerTTC>();
      foreach (var thing in this.membersMarkerTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<MarkerTTC> ClearAllMarkerTTC() {
      var result = new List<MarkerTTC>();
      this.membersMarkerTTCMutSet.Clear();
      return result;
    }
    public MarkerTTC GetOnlyMarkerTTCOrNull() {
      var result = GetAllMarkerTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return MarkerTTC.Null;
      }
    }
    public List<LevelLinkTTC> GetAllLevelLinkTTC() {
      var result = new List<LevelLinkTTC>();
      foreach (var thing in this.membersLevelLinkTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<LevelLinkTTC> ClearAllLevelLinkTTC() {
      var result = new List<LevelLinkTTC>();
      this.membersLevelLinkTTCMutSet.Clear();
      return result;
    }
    public LevelLinkTTC GetOnlyLevelLinkTTCOrNull() {
      var result = GetAllLevelLinkTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return LevelLinkTTC.Null;
      }
    }
    public List<MudTTC> GetAllMudTTC() {
      var result = new List<MudTTC>();
      foreach (var thing in this.membersMudTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<MudTTC> ClearAllMudTTC() {
      var result = new List<MudTTC>();
      this.membersMudTTCMutSet.Clear();
      return result;
    }
    public MudTTC GetOnlyMudTTCOrNull() {
      var result = GetAllMudTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return MudTTC.Null;
      }
    }
    public List<DirtTTC> GetAllDirtTTC() {
      var result = new List<DirtTTC>();
      foreach (var thing in this.membersDirtTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<DirtTTC> ClearAllDirtTTC() {
      var result = new List<DirtTTC>();
      this.membersDirtTTCMutSet.Clear();
      return result;
    }
    public DirtTTC GetOnlyDirtTTCOrNull() {
      var result = GetAllDirtTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return DirtTTC.Null;
      }
    }
    public List<ObsidianTTC> GetAllObsidianTTC() {
      var result = new List<ObsidianTTC>();
      foreach (var thing in this.membersObsidianTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<ObsidianTTC> ClearAllObsidianTTC() {
      var result = new List<ObsidianTTC>();
      this.membersObsidianTTCMutSet.Clear();
      return result;
    }
    public ObsidianTTC GetOnlyObsidianTTCOrNull() {
      var result = GetAllObsidianTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return ObsidianTTC.Null;
      }
    }
    public List<DownStairsTTC> GetAllDownStairsTTC() {
      var result = new List<DownStairsTTC>();
      foreach (var thing in this.membersDownStairsTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<DownStairsTTC> ClearAllDownStairsTTC() {
      var result = new List<DownStairsTTC>();
      this.membersDownStairsTTCMutSet.Clear();
      return result;
    }
    public DownStairsTTC GetOnlyDownStairsTTCOrNull() {
      var result = GetAllDownStairsTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return DownStairsTTC.Null;
      }
    }
    public List<UpStairsTTC> GetAllUpStairsTTC() {
      var result = new List<UpStairsTTC>();
      foreach (var thing in this.membersUpStairsTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<UpStairsTTC> ClearAllUpStairsTTC() {
      var result = new List<UpStairsTTC>();
      this.membersUpStairsTTCMutSet.Clear();
      return result;
    }
    public UpStairsTTC GetOnlyUpStairsTTCOrNull() {
      var result = GetAllUpStairsTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return UpStairsTTC.Null;
      }
    }
    public List<WallTTC> GetAllWallTTC() {
      var result = new List<WallTTC>();
      foreach (var thing in this.membersWallTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<WallTTC> ClearAllWallTTC() {
      var result = new List<WallTTC>();
      this.membersWallTTCMutSet.Clear();
      return result;
    }
    public WallTTC GetOnlyWallTTCOrNull() {
      var result = GetAllWallTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return WallTTC.Null;
      }
    }
    public List<BloodTTC> GetAllBloodTTC() {
      var result = new List<BloodTTC>();
      foreach (var thing in this.membersBloodTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<BloodTTC> ClearAllBloodTTC() {
      var result = new List<BloodTTC>();
      this.membersBloodTTCMutSet.Clear();
      return result;
    }
    public BloodTTC GetOnlyBloodTTCOrNull() {
      var result = GetAllBloodTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return BloodTTC.Null;
      }
    }
    public List<RocksTTC> GetAllRocksTTC() {
      var result = new List<RocksTTC>();
      foreach (var thing in this.membersRocksTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<RocksTTC> ClearAllRocksTTC() {
      var result = new List<RocksTTC>();
      this.membersRocksTTCMutSet.Clear();
      return result;
    }
    public RocksTTC GetOnlyRocksTTCOrNull() {
      var result = GetAllRocksTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return RocksTTC.Null;
      }
    }
    public List<TreeTTC> GetAllTreeTTC() {
      var result = new List<TreeTTC>();
      foreach (var thing in this.membersTreeTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<TreeTTC> ClearAllTreeTTC() {
      var result = new List<TreeTTC>();
      this.membersTreeTTCMutSet.Clear();
      return result;
    }
    public TreeTTC GetOnlyTreeTTCOrNull() {
      var result = GetAllTreeTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return TreeTTC.Null;
      }
    }
    public List<WaterTTC> GetAllWaterTTC() {
      var result = new List<WaterTTC>();
      foreach (var thing in this.membersWaterTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<WaterTTC> ClearAllWaterTTC() {
      var result = new List<WaterTTC>();
      this.membersWaterTTCMutSet.Clear();
      return result;
    }
    public WaterTTC GetOnlyWaterTTCOrNull() {
      var result = GetAllWaterTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return WaterTTC.Null;
      }
    }
    public List<FloorTTC> GetAllFloorTTC() {
      var result = new List<FloorTTC>();
      foreach (var thing in this.membersFloorTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<FloorTTC> ClearAllFloorTTC() {
      var result = new List<FloorTTC>();
      this.membersFloorTTCMutSet.Clear();
      return result;
    }
    public FloorTTC GetOnlyFloorTTCOrNull() {
      var result = GetAllFloorTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return FloorTTC.Null;
      }
    }
    public List<CaveWallTTC> GetAllCaveWallTTC() {
      var result = new List<CaveWallTTC>();
      foreach (var thing in this.membersCaveWallTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<CaveWallTTC> ClearAllCaveWallTTC() {
      var result = new List<CaveWallTTC>();
      this.membersCaveWallTTCMutSet.Clear();
      return result;
    }
    public CaveWallTTC GetOnlyCaveWallTTCOrNull() {
      var result = GetAllCaveWallTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return CaveWallTTC.Null;
      }
    }
    public List<CaveTTC> GetAllCaveTTC() {
      var result = new List<CaveTTC>();
      foreach (var thing in this.membersCaveTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<CaveTTC> ClearAllCaveTTC() {
      var result = new List<CaveTTC>();
      this.membersCaveTTCMutSet.Clear();
      return result;
    }
    public CaveTTC GetOnlyCaveTTCOrNull() {
      var result = GetAllCaveTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return CaveTTC.Null;
      }
    }
    public List<FallsTTC> GetAllFallsTTC() {
      var result = new List<FallsTTC>();
      foreach (var thing in this.membersFallsTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<FallsTTC> ClearAllFallsTTC() {
      var result = new List<FallsTTC>();
      this.membersFallsTTCMutSet.Clear();
      return result;
    }
    public FallsTTC GetOnlyFallsTTCOrNull() {
      var result = GetAllFallsTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return FallsTTC.Null;
      }
    }
    public List<MagmaTTC> GetAllMagmaTTC() {
      var result = new List<MagmaTTC>();
      foreach (var thing in this.membersMagmaTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<MagmaTTC> ClearAllMagmaTTC() {
      var result = new List<MagmaTTC>();
      this.membersMagmaTTCMutSet.Clear();
      return result;
    }
    public MagmaTTC GetOnlyMagmaTTCOrNull() {
      var result = GetAllMagmaTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return MagmaTTC.Null;
      }
    }
    public List<CliffTTC> GetAllCliffTTC() {
      var result = new List<CliffTTC>();
      foreach (var thing in this.membersCliffTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<CliffTTC> ClearAllCliffTTC() {
      var result = new List<CliffTTC>();
      this.membersCliffTTCMutSet.Clear();
      return result;
    }
    public CliffTTC GetOnlyCliffTTCOrNull() {
      var result = GetAllCliffTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return CliffTTC.Null;
      }
    }
    public List<RavaNestTTC> GetAllRavaNestTTC() {
      var result = new List<RavaNestTTC>();
      foreach (var thing in this.membersRavaNestTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<RavaNestTTC> ClearAllRavaNestTTC() {
      var result = new List<RavaNestTTC>();
      this.membersRavaNestTTCMutSet.Clear();
      return result;
    }
    public RavaNestTTC GetOnlyRavaNestTTCOrNull() {
      var result = GetAllRavaNestTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return RavaNestTTC.Null;
      }
    }
    public List<CliffLandingTTC> GetAllCliffLandingTTC() {
      var result = new List<CliffLandingTTC>();
      foreach (var thing in this.membersCliffLandingTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<CliffLandingTTC> ClearAllCliffLandingTTC() {
      var result = new List<CliffLandingTTC>();
      this.membersCliffLandingTTCMutSet.Clear();
      return result;
    }
    public CliffLandingTTC GetOnlyCliffLandingTTCOrNull() {
      var result = GetAllCliffLandingTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return CliffLandingTTC.Null;
      }
    }
    public List<StoneTTC> GetAllStoneTTC() {
      var result = new List<StoneTTC>();
      foreach (var thing in this.membersStoneTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<StoneTTC> ClearAllStoneTTC() {
      var result = new List<StoneTTC>();
      this.membersStoneTTCMutSet.Clear();
      return result;
    }
    public StoneTTC GetOnlyStoneTTCOrNull() {
      var result = GetAllStoneTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return StoneTTC.Null;
      }
    }
    public List<GrassTTC> GetAllGrassTTC() {
      var result = new List<GrassTTC>();
      foreach (var thing in this.membersGrassTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<GrassTTC> ClearAllGrassTTC() {
      var result = new List<GrassTTC>();
      this.membersGrassTTCMutSet.Clear();
      return result;
    }
    public GrassTTC GetOnlyGrassTTCOrNull() {
      var result = GetAllGrassTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return GrassTTC.Null;
      }
    }
    public List<IncendianFallsLevelLinkerTTC> GetAllIncendianFallsLevelLinkerTTC() {
      var result = new List<IncendianFallsLevelLinkerTTC>();
      foreach (var thing in this.membersIncendianFallsLevelLinkerTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<IncendianFallsLevelLinkerTTC> ClearAllIncendianFallsLevelLinkerTTC() {
      var result = new List<IncendianFallsLevelLinkerTTC>();
      this.membersIncendianFallsLevelLinkerTTCMutSet.Clear();
      return result;
    }
    public IncendianFallsLevelLinkerTTC GetOnlyIncendianFallsLevelLinkerTTCOrNull() {
      var result = GetAllIncendianFallsLevelLinkerTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return IncendianFallsLevelLinkerTTC.Null;
      }
    }
    public List<EmberDeepLevelLinkerTTC> GetAllEmberDeepLevelLinkerTTC() {
      var result = new List<EmberDeepLevelLinkerTTC>();
      foreach (var thing in this.membersEmberDeepLevelLinkerTTCMutSet) {
        result.Add(thing);
      }
      return result;
    }
    public List<EmberDeepLevelLinkerTTC> ClearAllEmberDeepLevelLinkerTTC() {
      var result = new List<EmberDeepLevelLinkerTTC>();
      this.membersEmberDeepLevelLinkerTTCMutSet.Clear();
      return result;
    }
    public EmberDeepLevelLinkerTTC GetOnlyEmberDeepLevelLinkerTTCOrNull() {
      var result = GetAllEmberDeepLevelLinkerTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return EmberDeepLevelLinkerTTC.Null;
      }
    }
    public List<IPresenceTriggerTTC> GetAllIPresenceTriggerTTC() {
      var result = new List<IPresenceTriggerTTC>();
      foreach (var obj in this.membersSimplePresenceTriggerTTCMutSet) {
        result.Add(
            new SimplePresenceTriggerTTCAsIPresenceTriggerTTC(obj));
      }
      return result;
    }
    public List<IPresenceTriggerTTC> ClearAllIPresenceTriggerTTC() {
      var result = new List<IPresenceTriggerTTC>();
      this.membersSimplePresenceTriggerTTCMutSet.Clear();
      return result;
    }
    public IPresenceTriggerTTC GetOnlyIPresenceTriggerTTCOrNull() {
      var result = GetAllIPresenceTriggerTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIPresenceTriggerTTC.Null;
      }
    }
                 public List<IInteractableTTC> GetAllIInteractableTTC() {
      var result = new List<IInteractableTTC>();
      foreach (var obj in this.membersWarperTTCMutSet) {
        result.Add(
            new WarperTTCAsIInteractableTTC(obj));
      }
      foreach (var obj in this.membersLevelLinkTTCMutSet) {
        result.Add(
            new LevelLinkTTCAsIInteractableTTC(obj));
      }
      foreach (var obj in this.membersItemTTCMutSet) {
        result.Add(
            new ItemTTCAsIInteractableTTC(obj));
      }
      foreach (var obj in this.membersIncendianFallsLevelLinkerTTCMutSet) {
        result.Add(
            new IncendianFallsLevelLinkerTTCAsIInteractableTTC(obj));
      }
      foreach (var obj in this.membersEmberDeepLevelLinkerTTCMutSet) {
        result.Add(
            new EmberDeepLevelLinkerTTCAsIInteractableTTC(obj));
      }
      return result;
    }
    public List<IInteractableTTC> ClearAllIInteractableTTC() {
      var result = new List<IInteractableTTC>();
      this.membersWarperTTCMutSet.Clear();
      this.membersLevelLinkTTCMutSet.Clear();
      this.membersItemTTCMutSet.Clear();
      this.membersIncendianFallsLevelLinkerTTCMutSet.Clear();
      this.membersEmberDeepLevelLinkerTTCMutSet.Clear();
      return result;
    }
    public IInteractableTTC GetOnlyIInteractableTTCOrNull() {
      var result = GetAllIInteractableTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIInteractableTTC.Null;
      }
    }
                 public List<IUnwalkableTTC> GetAllIUnwalkableTTC() {
      var result = new List<IUnwalkableTTC>();
      foreach (var obj in this.membersTreeTTCMutSet) {
        result.Add(
            new TreeTTCAsIUnwalkableTTC(obj));
      }
      foreach (var obj in this.membersWaterTTCMutSet) {
        result.Add(
            new WaterTTCAsIUnwalkableTTC(obj));
      }
      foreach (var obj in this.membersFloorTTCMutSet) {
        result.Add(
            new FloorTTCAsIUnwalkableTTC(obj));
      }
      foreach (var obj in this.membersCaveWallTTCMutSet) {
        result.Add(
            new CaveWallTTCAsIUnwalkableTTC(obj));
      }
      foreach (var obj in this.membersFallsTTCMutSet) {
        result.Add(
            new FallsTTCAsIUnwalkableTTC(obj));
      }
      foreach (var obj in this.membersMagmaTTCMutSet) {
        result.Add(
            new MagmaTTCAsIUnwalkableTTC(obj));
      }
      return result;
    }
    public List<IUnwalkableTTC> ClearAllIUnwalkableTTC() {
      var result = new List<IUnwalkableTTC>();
      this.membersTreeTTCMutSet.Clear();
      this.membersWaterTTCMutSet.Clear();
      this.membersFloorTTCMutSet.Clear();
      this.membersCaveWallTTCMutSet.Clear();
      this.membersFallsTTCMutSet.Clear();
      this.membersMagmaTTCMutSet.Clear();
      return result;
    }
    public IUnwalkableTTC GetOnlyIUnwalkableTTCOrNull() {
      var result = GetAllIUnwalkableTTC();
      Asserts.Assert(result.Count <= 1);
      if (result.Count > 0) {
        return result[0];
      } else {
        return NullIUnwalkableTTC.Null;
      }
    }
             }
}
