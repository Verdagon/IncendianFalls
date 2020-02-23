using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {

public interface ILogger {
  void Info(string str);
  void Warning(string str);
  void Error(string str);
}

public struct VersionAndIncarnation<T> {
  public int version;
  public T incarnation;
  public VersionAndIncarnation(int version, T incarnation) {
    this.version = version;
    this.incarnation = incarnation;
  }
}

public class Root {
  private static readonly int VERSION_HASH_MULTIPLIER = 179424673;
  private static readonly int NEXT_ID_HASH_MULTIPLIER = 373587883;

  private void CheckRootsEqual(Root a, Root b) {
    if (a != b) {
      throw new System.Exception("Given objects aren't from the same root!");
    }
  }

  public readonly ILogger logger;

  // This *always* points to a live RootIncarnation. When we snapshot, we eagerly
  // make a new one of these.
  private RootIncarnation rootIncarnation;

  bool locked;

  // 0 means everything

  readonly SortedDictionary<int, List<ITerrainTileEffectObserver>> observersForTerrainTile =
      new SortedDictionary<int, List<ITerrainTileEffectObserver>>();
  readonly List<TerrainTileCreateEffect> effectsTerrainTileCreateEffect =
      new List<TerrainTileCreateEffect>();
  readonly List<TerrainTileDeleteEffect> effectsTerrainTileDeleteEffect =
      new List<TerrainTileDeleteEffect>();
  readonly List<TerrainTileSetElevationEffect> effectsTerrainTileSetElevationEffect =
      new List<TerrainTileSetElevationEffect>();

  readonly SortedDictionary<int, List<ITerrainEffectObserver>> observersForTerrain =
      new SortedDictionary<int, List<ITerrainEffectObserver>>();
  readonly List<TerrainCreateEffect> effectsTerrainCreateEffect =
      new List<TerrainCreateEffect>();
  readonly List<TerrainDeleteEffect> effectsTerrainDeleteEffect =
      new List<TerrainDeleteEffect>();

  readonly SortedDictionary<int, List<ILevelEffectObserver>> observersForLevel =
      new SortedDictionary<int, List<ILevelEffectObserver>>();
  readonly List<LevelCreateEffect> effectsLevelCreateEffect =
      new List<LevelCreateEffect>();
  readonly List<LevelDeleteEffect> effectsLevelDeleteEffect =
      new List<LevelDeleteEffect>();

  readonly SortedDictionary<int, List<IRandEffectObserver>> observersForRand =
      new SortedDictionary<int, List<IRandEffectObserver>>();
  readonly List<RandCreateEffect> effectsRandCreateEffect =
      new List<RandCreateEffect>();
  readonly List<RandDeleteEffect> effectsRandDeleteEffect =
      new List<RandDeleteEffect>();
  readonly List<RandSetRandEffect> effectsRandSetRandEffect =
      new List<RandSetRandEffect>();

  readonly SortedDictionary<int, List<IStrMutListEffectObserver>> observersForStrMutList =
      new SortedDictionary<int, List<IStrMutListEffectObserver>>();
  readonly List<StrMutListCreateEffect> effectsStrMutListCreateEffect =
      new List<StrMutListCreateEffect>();
  readonly List<StrMutListDeleteEffect> effectsStrMutListDeleteEffect =
      new List<StrMutListDeleteEffect>();
  readonly List<StrMutListAddEffect> effectsStrMutListAddEffect =
      new List<StrMutListAddEffect>();
  readonly List<StrMutListRemoveEffect> effectsStrMutListRemoveEffect =
      new List<StrMutListRemoveEffect>();

  readonly SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>> observersForTerrainTileByLocationMutMap =
      new SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>>();
  readonly List<TerrainTileByLocationMutMapCreateEffect> effectsTerrainTileByLocationMutMapCreateEffect =
      new List<TerrainTileByLocationMutMapCreateEffect>();
  readonly List<TerrainTileByLocationMutMapDeleteEffect> effectsTerrainTileByLocationMutMapDeleteEffect =
      new List<TerrainTileByLocationMutMapDeleteEffect>();
  readonly List<TerrainTileByLocationMutMapAddEffect> effectsTerrainTileByLocationMutMapAddEffect =
      new List<TerrainTileByLocationMutMapAddEffect>();
  readonly List<TerrainTileByLocationMutMapRemoveEffect> effectsTerrainTileByLocationMutMapRemoveEffect =
      new List<TerrainTileByLocationMutMapRemoveEffect>();

  public Root(ILogger logger) {
    this.logger = logger;
    int initialVersion = 1;
    int initialNextId = 1;
    int initialHash = VERSION_HASH_MULTIPLIER * initialVersion + NEXT_ID_HASH_MULTIPLIER * initialNextId;
    rootIncarnation = new RootIncarnation(initialVersion, initialNextId, initialHash);
    this.locked = true;
  }

  public Root(ILogger logger, RootIncarnation rootIncarnation) {
    this.logger = logger;
    this.rootIncarnation = rootIncarnation;
    this.locked = false;
    this.Snapshot();
    this.locked = true;
  }

  public int version { get { return rootIncarnation.version; } }

  public RootIncarnation Snapshot() {
    CheckUnlocked();
    RootIncarnation oldIncarnation = rootIncarnation;
    int newHash = oldIncarnation.hash;
    int newVersion = oldIncarnation.version + 1;
    rootIncarnation =
        new RootIncarnation(
            newVersion, oldIncarnation.nextId, newHash, oldIncarnation);
    return oldIncarnation;
  }

  public delegate T ITransaction<T>();

  public T Transact<T>(ITransaction<T> transaction) {
    var stopwatch = new System.Diagnostics.Stopwatch();
    stopwatch.Start();

    if (!locked) {
      throw new Exception("Can't unlock, not locked!");
    }
    locked = false;
    // var rollbackPoint = Snapshot();
    try {
      return transaction();
    } catch (Exception e) {
      // logger.Error("Rolling back because of error: " + e.Message + "\n" + e.StackTrace);
      // Revert(rollbackPoint);
      logger.Error("Encountered error in transaction: " + e.Message + "\n" + e.StackTrace);
      throw;
    } finally {
      if (locked) {
        logger.Error("Can't lock, already locked!");
        Environment.Exit(1);
      }
      locked = true;
      // CheckForViolations();

      stopwatch.Stop();
      logger.Info("Transaction run time " + stopwatch.Elapsed.TotalMilliseconds);
      FlushEvents();
    }
  }

  public void CheckUnlocked() {
    if (locked) {
      throw new Exception("Can't proceed, superstructure is locked!");
    }
  }

  private int NewId() {
    this.UpdateHashOnNextIdChange(rootIncarnation.nextId, rootIncarnation.nextId + 1);
    return rootIncarnation.nextId++;
  }

  private void UpdateHashOnNextIdChange(int oldNextId, int newNextId) {
    int oldIdAndVersionHashContribution =
        VERSION_HASH_MULTIPLIER * rootIncarnation.version +
        NEXT_ID_HASH_MULTIPLIER * oldNextId;
    int newIdAndVersionHashContribution =
        VERSION_HASH_MULTIPLIER * rootIncarnation.version +
        NEXT_ID_HASH_MULTIPLIER * newNextId;
    rootIncarnation.hash =
        rootIncarnation.hash -
        oldIdAndVersionHashContribution +
        newIdAndVersionHashContribution;
  }

  private int RecalculateEntireHash() {
    int result =
        VERSION_HASH_MULTIPLIER * rootIncarnation.version +
        NEXT_ID_HASH_MULTIPLIER * rootIncarnation.nextId;

    foreach (var entry in this.rootIncarnation.incarnationsTerrainTile) {
      result += GetTerrainTileHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTerrain) {
      result += GetTerrainHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsLevel) {
      result += GetLevelHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsRand) {
      result += GetRandHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsStrMutList) {
      result += GetStrMutListHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    foreach (var entry in this.rootIncarnation.incarnationsTerrainTileByLocationMutMap) {
      result += GetTerrainTileByLocationMutMapHash(entry.Key, entry.Value.version, entry.Value.incarnation);
    }
    return result;
  }

  public void CheckForViolations() {
    List<string> violations = new List<string>();

    foreach (var obj in this.AllTerrainTile()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllTerrain()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllLevel()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllRand()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllStrMutList()) {
      obj.CheckForNullViolations(violations);
    }
    foreach (var obj in this.AllTerrainTileByLocationMutMap()) {
      obj.CheckForNullViolations(violations);
    }

    SortedSet<int> reachableIds = new SortedSet<int>();
    foreach (var rootStruct in this.AllLevel()) {
      rootStruct.FindReachableObjects(reachableIds);
    }
    foreach (var obj in this.AllTerrainTile()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllTerrain()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllLevel()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllRand()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllStrMutList()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }
    foreach (var obj in this.AllTerrainTileByLocationMutMap()) {
      if (!reachableIds.Contains(obj.id)) {
        violations.Add("Unreachable: " + obj + "#" + obj.id);
      }
    }

    if (violations.Count > 0) {
      string message = "Found violations!\n";
      foreach (var violation in violations) {
        message += violation + "\n";
      }
      throw new Exception(message);
    }
  }

  public void FlushEvents() {



    var copyOfObserversForTerrainTile =
        new SortedDictionary<int, List<ITerrainTileEffectObserver>>();
    foreach (var entry in observersForTerrainTile) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTerrainTile.Add(
          objectId,
          new List<ITerrainTileEffectObserver>(
              observers));
    }

    var copyOfObserversForTerrain =
        new SortedDictionary<int, List<ITerrainEffectObserver>>();
    foreach (var entry in observersForTerrain) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTerrain.Add(
          objectId,
          new List<ITerrainEffectObserver>(
              observers));
    }

    var copyOfObserversForLevel =
        new SortedDictionary<int, List<ILevelEffectObserver>>();
    foreach (var entry in observersForLevel) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForLevel.Add(
          objectId,
          new List<ILevelEffectObserver>(
              observers));
    }

    var copyOfObserversForRand =
        new SortedDictionary<int, List<IRandEffectObserver>>();
    foreach (var entry in observersForRand) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForRand.Add(
          objectId,
          new List<IRandEffectObserver>(
              observers));
    }

    var copyOfObserversForStrMutList =
        new SortedDictionary<int, List<IStrMutListEffectObserver>>();
    foreach (var entry in observersForStrMutList) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForStrMutList.Add(
          objectId,
          new List<IStrMutListEffectObserver>(
              observers));
    }

    var copyOfObserversForTerrainTileByLocationMutMap =
        new SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>>();
    foreach (var entry in observersForTerrainTileByLocationMutMap) {
      var objectId = entry.Key;
      var observers = entry.Value;
      copyOfObserversForTerrainTileByLocationMutMap.Add(
          objectId,
          new List<ITerrainTileByLocationMutMapEffectObserver>(
              observers));
    }

    BroadcastTerrainTileEffects(
        copyOfObserversForTerrainTile);
           
    BroadcastTerrainEffects(
        copyOfObserversForTerrain);
           
    BroadcastLevelEffects(
        copyOfObserversForLevel);
           
    BroadcastRandEffects(
        copyOfObserversForRand);
           
    BroadcastStrMutListEffects(
        copyOfObserversForStrMutList);
           
    BroadcastTerrainTileByLocationMutMapEffects(
        copyOfObserversForTerrainTileByLocationMutMap);
           
  }

  public int GetDeterministicHashCode() {
    // int doubleCheckHash = RecalculateEntireHash();
    // Asserts.Assert(doubleCheckHash == this.rootIncarnation.hash);
    return this.rootIncarnation.hash;
  }

  public void Revert(RootIncarnation sourceIncarnation) {
    CheckUnlocked();
    // We do all the adds first so that we don't violate any strong borrows.
    // Then we do all the changes, because those might be flipping things to point
    // at things that were just made.
    // Then we do all the removes.


    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrainTile) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTerrainTile.ContainsKey(sourceObjId)) {
        EffectInternalCreateTerrainTile(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrain) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTerrain.ContainsKey(sourceObjId)) {
        EffectInternalCreateTerrain(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLevel) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsLevel.ContainsKey(sourceObjId)) {
        EffectInternalCreateLevel(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsRand) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsRand.ContainsKey(sourceObjId)) {
        EffectInternalCreateRand(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsStrMutList) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsStrMutList.ContainsKey(sourceObjId)) {
        EffectInternalCreateStrMutList(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrainTileByLocationMutMap) {
      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (!rootIncarnation.incarnationsTerrainTileByLocationMutMap.ContainsKey(sourceObjId)) {
        EffectInternalCreateTerrainTileByLocationMutMap(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
      }
    }
         
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsStrMutList) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsStrMutList.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsStrMutList[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            for (int i = currentObjIncarnation.list.Count - 1; i >= 0; i--) {
              EffectStrMutListRemoveAt(objId, i);
            }
            foreach (var objIdInSourceObjIncarnation in sourceObjIncarnation.list) {
              EffectStrMutListAdd(objId, objIdInSourceObjIncarnation);
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
                  rootIncarnation.incarnationsStrMutList[objId] = sourceVersionAndObjIncarnation;

          }
        }
      }
             
      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrainTileByLocationMutMap) {
        var objId = sourceIdAndVersionAndObjIncarnation.Key;
        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
        var sourceVersion = sourceVersionAndObjIncarnation.version;
        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
        if (rootIncarnation.incarnationsTerrainTileByLocationMutMap.ContainsKey(objId)) {
          // Compare everything that could possibly have changed.
          var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTerrainTileByLocationMutMap[objId];
          var currentVersion = currentVersionAndObjIncarnation.version;
          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
          if (currentVersion != sourceVersion) {
            foreach (var entryInCurrentObjIncarnation in new SortedDictionary<Location, int>(currentObjIncarnation.map)) {
              var key = entryInCurrentObjIncarnation.Key;
              if (!sourceObjIncarnation.map.ContainsKey(key)) {
                EffectTerrainTileByLocationMutMapRemove(objId, key);
              }
            }
            foreach (var entryInSourceObjIncarnation in sourceObjIncarnation.map) {
              var key = entryInSourceObjIncarnation.Key;
              var element = entryInSourceObjIncarnation.Value;
              if (!currentObjIncarnation.map.ContainsKey(key)) {
                EffectTerrainTileByLocationMutMapAdd(objId, key, element);
              }
            }
            // Swap out the underlying incarnation. The only visible effect this has is
            // changing the version number.
            rootIncarnation.incarnationsTerrainTileByLocationMutMap[objId] = sourceVersionAndObjIncarnation;
          }
        }
      }
             
    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrainTile) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsTerrainTile.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTerrainTile[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          if (sourceObjIncarnation.elevation != currentObjIncarnation.elevation) {
            EffectTerrainTileSetElevation(objId, sourceObjIncarnation.elevation);
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsTerrainTile[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsTerrain) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsTerrain.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsTerrain[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsTerrain[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsLevel) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsLevel.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsLevel[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsLevel[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnationsRand) {
      var objId = sourceIdAndVersionAndObjIncarnation.Key;
      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
      var sourceVersion = sourceVersionAndObjIncarnation.version;
      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
      if (rootIncarnation.incarnationsRand.ContainsKey(objId)) {
        // Compare everything that could possibly have changed.
        var currentVersionAndObjIncarnation = rootIncarnation.incarnationsRand[objId];
        var currentVersion = currentVersionAndObjIncarnation.version;
        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
        if (currentVersion != sourceVersion) {

          if (sourceObjIncarnation.rand != currentObjIncarnation.rand) {
            EffectRandSetRand(objId, sourceObjIncarnation.rand);
          }

          // Swap out the underlying incarnation. The only visible effect this has is
          // changing the version number.
          
          rootIncarnation.incarnationsRand[objId] = sourceVersionAndObjIncarnation;
          
        }
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>>(rootIncarnation.incarnationsTerrainTile)) {
      if (!sourceIncarnation.incarnationsTerrainTile.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTerrainTileDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>>(rootIncarnation.incarnationsTerrain)) {
      if (!sourceIncarnation.incarnationsTerrain.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTerrainDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>>(rootIncarnation.incarnationsLevel)) {
      if (!sourceIncarnation.incarnationsLevel.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectLevelDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<RandIncarnation>>(rootIncarnation.incarnationsRand)) {
      if (!sourceIncarnation.incarnationsRand.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectRandDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<StrMutListIncarnation>>(rootIncarnation.incarnationsStrMutList)) {
      if (!sourceIncarnation.incarnationsStrMutList.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectStrMutListDelete(id);
      }
    }

    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>>(rootIncarnation.incarnationsTerrainTileByLocationMutMap)) {
      if (!sourceIncarnation.incarnationsTerrainTileByLocationMutMap.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
        var id = currentIdAndVersionAndObjIncarnation.Key;
        EffectTerrainTileByLocationMutMapDelete(id);
      }
    }

  }
       public TerrainTileIncarnation GetTerrainTileIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsTerrainTile[id].incarnation;
  }
  public bool TerrainTileExists(int id) {
    return rootIncarnation.incarnationsTerrainTile.ContainsKey(id);
  }
  public TerrainTile GetTerrainTile(int id) {
    return new TerrainTile(this, id);
  }
  public List<TerrainTile> AllTerrainTile() {
    List<TerrainTile> result = new List<TerrainTile>(rootIncarnation.incarnationsTerrainTile.Count);
    foreach (var id in rootIncarnation.incarnationsTerrainTile.Keys) {
      result.Add(new TerrainTile(this, id));
    }
    return result;
  }
  public IEnumerator<TerrainTile> EnumAllTerrainTile() {
    foreach (var id in rootIncarnation.incarnationsTerrainTile.Keys) {
      yield return GetTerrainTile(id);
    }
  }
  public void CheckHasTerrainTile(TerrainTile thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasTerrainTile(thing.id);
  }
  public void CheckHasTerrainTile(int id) {
    if (!rootIncarnation.incarnationsTerrainTile.ContainsKey(id)) {
      throw new System.Exception("Invalid TerrainTile: " + id);
    }
  }
  public void AddTerrainTileObserver(int id, ITerrainTileEffectObserver observer) {
    List<ITerrainTileEffectObserver> obsies;
    if (!observersForTerrainTile.TryGetValue(id, out obsies)) {
      obsies = new List<ITerrainTileEffectObserver>();
    }
    obsies.Add(observer);
    observersForTerrainTile[id] = obsies;
  }

  public void RemoveTerrainTileObserver(int id, ITerrainTileEffectObserver observer) {
    if (observersForTerrainTile.ContainsKey(id)) {
      var list = observersForTerrainTile[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTerrainTile.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public TerrainTile EffectTerrainTileCreate(
      int elevation,
      StrMutList members) {
    CheckUnlocked();
    CheckHasStrMutList(members);

    var id = NewId();
    var incarnation =
        new TerrainTileIncarnation(
            elevation,
            members.id
            );
    EffectInternalCreateTerrainTile(id, rootIncarnation.version, incarnation);
    return new TerrainTile(this, id);
  }
  public void EffectInternalCreateTerrainTile(
      int id,
      int incarnationVersion,
      TerrainTileIncarnation incarnation) {
    CheckUnlocked();
    var effect = new TerrainTileCreateEffect(id);
    rootIncarnation.incarnationsTerrainTile.Add(
        id,
        new VersionAndIncarnation<TerrainTileIncarnation>(
            incarnationVersion,
            incarnation));
    effectsTerrainTileCreateEffect.Add(effect);
  }

  public void EffectTerrainTileDelete(int id) {
    CheckUnlocked();
    var effect = new TerrainTileDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsTerrainTile[id];

    rootIncarnation.incarnationsTerrainTile.Remove(id);
    effectsTerrainTileDeleteEffect.Add(effect);
  }

     
  public int GetTerrainTileHash(int id, int version, TerrainTileIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.elevation.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.members.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastTerrainTileEffects(
      SortedDictionary<int, List<ITerrainTileEffectObserver>> observers) {
    foreach (var effect in effectsTerrainTileDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileEffect(effect);
        }
        observersForTerrainTile.Remove(effect.id);
      }
    }
    effectsTerrainTileDeleteEffect.Clear();


    foreach (var effect in effectsTerrainTileSetElevationEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileEffect(effect);
        }
      }
    }
    effectsTerrainTileSetElevationEffect.Clear();

    foreach (var effect in effectsTerrainTileCreateEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileEffect(effect);
        }
      }
    }
    effectsTerrainTileCreateEffect.Clear();
  }

  public void EffectTerrainTileSetElevation(int id, int newValue) {
    CheckUnlocked();
    CheckHasTerrainTile(id);
    var effect = new TerrainTileSetElevationEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrainTile[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.elevation;
      oldIncarnationAndVersion.incarnation.elevation = newValue;

    } else {
      var newIncarnation =
          new TerrainTileIncarnation(
              newValue,
              oldIncarnationAndVersion.incarnation.members);
      rootIncarnation.incarnationsTerrainTile[id] =
          new VersionAndIncarnation<TerrainTileIncarnation>(
              rootIncarnation.version,
              newIncarnation);
    }

    effectsTerrainTileSetElevationEffect.Add(effect);
  }
  public TerrainIncarnation GetTerrainIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsTerrain[id].incarnation;
  }
  public bool TerrainExists(int id) {
    return rootIncarnation.incarnationsTerrain.ContainsKey(id);
  }
  public Terrain GetTerrain(int id) {
    return new Terrain(this, id);
  }
  public List<Terrain> AllTerrain() {
    List<Terrain> result = new List<Terrain>(rootIncarnation.incarnationsTerrain.Count);
    foreach (var id in rootIncarnation.incarnationsTerrain.Keys) {
      result.Add(new Terrain(this, id));
    }
    return result;
  }
  public IEnumerator<Terrain> EnumAllTerrain() {
    foreach (var id in rootIncarnation.incarnationsTerrain.Keys) {
      yield return GetTerrain(id);
    }
  }
  public void CheckHasTerrain(Terrain thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasTerrain(thing.id);
  }
  public void CheckHasTerrain(int id) {
    if (!rootIncarnation.incarnationsTerrain.ContainsKey(id)) {
      throw new System.Exception("Invalid Terrain: " + id);
    }
  }
  public void AddTerrainObserver(int id, ITerrainEffectObserver observer) {
    List<ITerrainEffectObserver> obsies;
    if (!observersForTerrain.TryGetValue(id, out obsies)) {
      obsies = new List<ITerrainEffectObserver>();
    }
    obsies.Add(observer);
    observersForTerrain[id] = obsies;
  }

  public void RemoveTerrainObserver(int id, ITerrainEffectObserver observer) {
    if (observersForTerrain.ContainsKey(id)) {
      var list = observersForTerrain[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForTerrain.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public Terrain EffectTerrainCreate(
      Pattern pattern,
      float elevationStepHeight,
      TerrainTileByLocationMutMap tiles) {
    CheckUnlocked();
    CheckHasTerrainTileByLocationMutMap(tiles);

    var id = NewId();
    var incarnation =
        new TerrainIncarnation(
            pattern,
            elevationStepHeight,
            tiles.id
            );
    EffectInternalCreateTerrain(id, rootIncarnation.version, incarnation);
    return new Terrain(this, id);
  }
  public void EffectInternalCreateTerrain(
      int id,
      int incarnationVersion,
      TerrainIncarnation incarnation) {
    CheckUnlocked();
    var effect = new TerrainCreateEffect(id);
    rootIncarnation.incarnationsTerrain.Add(
        id,
        new VersionAndIncarnation<TerrainIncarnation>(
            incarnationVersion,
            incarnation));
    effectsTerrainCreateEffect.Add(effect);
  }

  public void EffectTerrainDelete(int id) {
    CheckUnlocked();
    var effect = new TerrainDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsTerrain[id];

    rootIncarnation.incarnationsTerrain.Remove(id);
    effectsTerrainDeleteEffect.Add(effect);
  }

     
  public int GetTerrainHash(int id, int version, TerrainIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.pattern.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.elevationStepHeight.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.tiles.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastTerrainEffects(
      SortedDictionary<int, List<ITerrainEffectObserver>> observers) {
    foreach (var effect in effectsTerrainDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITerrainEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainEffect(effect);
        }
        observersForTerrain.Remove(effect.id);
      }
    }
    effectsTerrainDeleteEffect.Clear();


    foreach (var effect in effectsTerrainCreateEffect) {
      if (observers.TryGetValue(0, out List<ITerrainEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainEffect(effect);
        }
      }
    }
    effectsTerrainCreateEffect.Clear();
  }
  public LevelIncarnation GetLevelIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsLevel[id].incarnation;
  }
  public bool LevelExists(int id) {
    return rootIncarnation.incarnationsLevel.ContainsKey(id);
  }
  public Level GetLevel(int id) {
    return new Level(this, id);
  }
  public List<Level> AllLevel() {
    List<Level> result = new List<Level>(rootIncarnation.incarnationsLevel.Count);
    foreach (var id in rootIncarnation.incarnationsLevel.Keys) {
      result.Add(new Level(this, id));
    }
    return result;
  }
  public IEnumerator<Level> EnumAllLevel() {
    foreach (var id in rootIncarnation.incarnationsLevel.Keys) {
      yield return GetLevel(id);
    }
  }
  public void CheckHasLevel(Level thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasLevel(thing.id);
  }
  public void CheckHasLevel(int id) {
    if (!rootIncarnation.incarnationsLevel.ContainsKey(id)) {
      throw new System.Exception("Invalid Level: " + id);
    }
  }
  public void AddLevelObserver(int id, ILevelEffectObserver observer) {
    List<ILevelEffectObserver> obsies;
    if (!observersForLevel.TryGetValue(id, out obsies)) {
      obsies = new List<ILevelEffectObserver>();
    }
    obsies.Add(observer);
    observersForLevel[id] = obsies;
  }

  public void RemoveLevelObserver(int id, ILevelEffectObserver observer) {
    if (observersForLevel.ContainsKey(id)) {
      var list = observersForLevel[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForLevel.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public Level EffectLevelCreate(
      Terrain terrain) {
    CheckUnlocked();
    CheckHasTerrain(terrain);

    var id = NewId();
    var incarnation =
        new LevelIncarnation(
            terrain.id
            );
    EffectInternalCreateLevel(id, rootIncarnation.version, incarnation);
    return new Level(this, id);
  }
  public void EffectInternalCreateLevel(
      int id,
      int incarnationVersion,
      LevelIncarnation incarnation) {
    CheckUnlocked();
    var effect = new LevelCreateEffect(id);
    rootIncarnation.incarnationsLevel.Add(
        id,
        new VersionAndIncarnation<LevelIncarnation>(
            incarnationVersion,
            incarnation));
    effectsLevelCreateEffect.Add(effect);
  }

  public void EffectLevelDelete(int id) {
    CheckUnlocked();
    var effect = new LevelDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsLevel[id];

    rootIncarnation.incarnationsLevel.Remove(id);
    effectsLevelDeleteEffect.Add(effect);
  }

     
  public int GetLevelHash(int id, int version, LevelIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.terrain.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastLevelEffects(
      SortedDictionary<int, List<ILevelEffectObserver>> observers) {
    foreach (var effect in effectsLevelDeleteEffect) {
      if (observers.TryGetValue(0, out List<ILevelEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnLevelEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ILevelEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnLevelEffect(effect);
        }
        observersForLevel.Remove(effect.id);
      }
    }
    effectsLevelDeleteEffect.Clear();


    foreach (var effect in effectsLevelCreateEffect) {
      if (observers.TryGetValue(0, out List<ILevelEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnLevelEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ILevelEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnLevelEffect(effect);
        }
      }
    }
    effectsLevelCreateEffect.Clear();
  }
  public RandIncarnation GetRandIncarnation(int id) {
    if (id == 0) {
      throw new Exception("Tried dereferencing null!");
    }
    return rootIncarnation.incarnationsRand[id].incarnation;
  }
  public bool RandExists(int id) {
    return rootIncarnation.incarnationsRand.ContainsKey(id);
  }
  public Rand GetRand(int id) {
    return new Rand(this, id);
  }
  public List<Rand> AllRand() {
    List<Rand> result = new List<Rand>(rootIncarnation.incarnationsRand.Count);
    foreach (var id in rootIncarnation.incarnationsRand.Keys) {
      result.Add(new Rand(this, id));
    }
    return result;
  }
  public IEnumerator<Rand> EnumAllRand() {
    foreach (var id in rootIncarnation.incarnationsRand.Keys) {
      yield return GetRand(id);
    }
  }
  public void CheckHasRand(Rand thing) {
    CheckRootsEqual(this, thing.root);
    CheckHasRand(thing.id);
  }
  public void CheckHasRand(int id) {
    if (!rootIncarnation.incarnationsRand.ContainsKey(id)) {
      throw new System.Exception("Invalid Rand: " + id);
    }
  }
  public void AddRandObserver(int id, IRandEffectObserver observer) {
    List<IRandEffectObserver> obsies;
    if (!observersForRand.TryGetValue(id, out obsies)) {
      obsies = new List<IRandEffectObserver>();
    }
    obsies.Add(observer);
    observersForRand[id] = obsies;
  }

  public void RemoveRandObserver(int id, IRandEffectObserver observer) {
    if (observersForRand.ContainsKey(id)) {
      var list = observersForRand[id];
      list.Remove(observer);
      if (list.Count == 0) {
        observersForRand.Remove(id);
      }
    } else {
      throw new Exception("Couldnt find!");
    }
  }
  public Rand EffectRandCreate(
      int rand) {
    CheckUnlocked();

    var id = NewId();
    var incarnation =
        new RandIncarnation(
            rand
            );
    EffectInternalCreateRand(id, rootIncarnation.version, incarnation);
    return new Rand(this, id);
  }
  public void EffectInternalCreateRand(
      int id,
      int incarnationVersion,
      RandIncarnation incarnation) {
    CheckUnlocked();
    var effect = new RandCreateEffect(id);
    rootIncarnation.incarnationsRand.Add(
        id,
        new VersionAndIncarnation<RandIncarnation>(
            incarnationVersion,
            incarnation));
    effectsRandCreateEffect.Add(effect);
  }

  public void EffectRandDelete(int id) {
    CheckUnlocked();
    var effect = new RandDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsRand[id];

    rootIncarnation.incarnationsRand.Remove(id);
    effectsRandDeleteEffect.Add(effect);
  }

     
  public int GetRandHash(int id, int version, RandIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.rand.GetDeterministicHashCode();
    return result;
  }
     
  public void BroadcastRandEffects(
      SortedDictionary<int, List<IRandEffectObserver>> observers) {
    foreach (var effect in effectsRandDeleteEffect) {
      if (observers.TryGetValue(0, out List<IRandEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnRandEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IRandEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnRandEffect(effect);
        }
        observersForRand.Remove(effect.id);
      }
    }
    effectsRandDeleteEffect.Clear();


    foreach (var effect in effectsRandSetRandEffect) {
      if (observers.TryGetValue(0, out List<IRandEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnRandEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IRandEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnRandEffect(effect);
        }
      }
    }
    effectsRandSetRandEffect.Clear();

    foreach (var effect in effectsRandCreateEffect) {
      if (observers.TryGetValue(0, out List<IRandEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnRandEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IRandEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnRandEffect(effect);
        }
      }
    }
    effectsRandCreateEffect.Clear();
  }

  public void EffectRandSetRand(int id, int newValue) {
    CheckUnlocked();
    CheckHasRand(id);
    var effect = new RandSetRandEffect(id, newValue);
    var oldIncarnationAndVersion = rootIncarnation.incarnationsRand[id];
    if (oldIncarnationAndVersion.version == rootIncarnation.version) {
      var oldValue = oldIncarnationAndVersion.incarnation.rand;
      oldIncarnationAndVersion.incarnation.rand = newValue;

    } else {
      var newIncarnation =
          new RandIncarnation(
              newValue);
      rootIncarnation.incarnationsRand[id] =
          new VersionAndIncarnation<RandIncarnation>(
              rootIncarnation.version,
              newIncarnation);
    }

    effectsRandSetRandEffect.Add(effect);
  }

    public int GetStrMutListHash(int id, int version, StrMutListIncarnation incarnation) {
      int result = id * version;
      foreach (var element in incarnation.list) {
        result += id * version * element.GetDeterministicHashCode();
      }
      return result;
    }
    public StrMutListIncarnation GetStrMutListIncarnation(int id) {
      return rootIncarnation.incarnationsStrMutList[id].incarnation;
    }
    public StrMutList GetStrMutList(int id) {
      return new StrMutList(this, id);
    }
    public List<StrMutList> AllStrMutList() {
      List<StrMutList> result = new List<StrMutList>(rootIncarnation.incarnationsStrMutList.Count);
      foreach (var id in rootIncarnation.incarnationsStrMutList.Keys) {
        result.Add(new StrMutList(this, id));
      }
      return result;
    }
    public bool StrMutListExists(int id) {
      return rootIncarnation.incarnationsStrMutList.ContainsKey(id);
    }
    public void CheckHasStrMutList(StrMutList thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasStrMutList(thing.id);
    }
    public void CheckHasStrMutList(int id) {
      if (!rootIncarnation.incarnationsStrMutList.ContainsKey(id)) {
        throw new System.Exception("Invalid StrMutList}: " + id);
      }
    }
    public StrMutList EffectStrMutListCreate() {
      CheckUnlocked();
      var id = NewId();
      EffectInternalCreateStrMutList(id, rootIncarnation.version, new StrMutListIncarnation(new List<string>()));
      return new StrMutList(this, id);
    }
    public StrMutList EffectStrMutListCreate(IEnumerable<string> elements) {
      var id = NewId();
      var incarnation = new StrMutListIncarnation(new List<string>(elements));
      EffectInternalCreateStrMutList(id, rootIncarnation.version, incarnation);
      return new StrMutList(this, id);
    }
    public void EffectInternalCreateStrMutList(int id, int incarnationVersion, StrMutListIncarnation incarnation) {
      var effect = new StrMutListCreateEffect(id);
      rootIncarnation.incarnationsStrMutList
          .Add(
              id,
              new VersionAndIncarnation<StrMutListIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsStrMutListCreateEffect.Add(effect);
    }
    public void EffectStrMutListDelete(int id) {
      CheckUnlocked();
      var effect = new StrMutListDeleteEffect(id);
      effectsStrMutListDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsStrMutList[id];
      rootIncarnation.incarnationsStrMutList.Remove(id);
    }
    public void EffectStrMutListAdd(int listId, string element) {
      CheckUnlocked();
      CheckHasStrMutList(listId);

    
      var effect = new StrMutListAddEffect(listId, element);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsStrMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.Add(element);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<string>(oldMap);
        newMap.Add(element);
        var newIncarnation = new StrMutListIncarnation(newMap);
        rootIncarnation.incarnationsStrMutList[listId] =
            new VersionAndIncarnation<StrMutListIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsStrMutListAddEffect.Add(effect);
    }
    public void EffectStrMutListRemoveAt(int listId, int index) {
      CheckUnlocked();
      CheckHasStrMutList(listId);

      var effect = new StrMutListRemoveEffect(listId, index);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsStrMutList[listId];
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        var oldElement = oldIncarnationAndVersion.incarnation.list[index];
        oldIncarnationAndVersion.incarnation.list.RemoveAt(index);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<string>(oldMap);
        newMap.RemoveAt(index);
        var newIncarnation = new StrMutListIncarnation(newMap);
        rootIncarnation.incarnationsStrMutList[listId] =
            new VersionAndIncarnation<StrMutListIncarnation>(
                rootIncarnation.version, newIncarnation);

      }
      effectsStrMutListRemoveEffect.Add(effect);
    }
       
    public void AddStrMutListObserver(int id, IStrMutListEffectObserver observer) {
      List<IStrMutListEffectObserver> obsies;
      if (!observersForStrMutList.TryGetValue(id, out obsies)) {
        obsies = new List<IStrMutListEffectObserver>();
      }
      obsies.Add(observer);
      observersForStrMutList[id] = obsies;
    }

    public void RemoveStrMutListObserver(int id, IStrMutListEffectObserver observer) {
      if (observersForStrMutList.ContainsKey(id)) {
        var list = observersForStrMutList[id];
        list.Remove(observer);
        if (list.Count == 0) {
          observersForStrMutList.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

  public void BroadcastStrMutListEffects(
      SortedDictionary<int, List<IStrMutListEffectObserver>> observers) {
    foreach (var effect in effectsStrMutListDeleteEffect) {
      if (observers.TryGetValue(0, out List<IStrMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStrMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStrMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStrMutListEffect(effect);
        }
        observersForStrMutList.Remove(effect.id);
      }
    }
    effectsStrMutListDeleteEffect.Clear();

    foreach (var effect in effectsStrMutListAddEffect) {
      if (observers.TryGetValue(0, out List<IStrMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStrMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStrMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStrMutListEffect(effect);
        }
      }
    }
    effectsStrMutListAddEffect.Clear();

    foreach (var effect in effectsStrMutListRemoveEffect) {
      if (observers.TryGetValue(0, out List<IStrMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStrMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStrMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStrMutListEffect(effect);
        }
      }
    }
    effectsStrMutListRemoveEffect.Clear();

    foreach (var effect in effectsStrMutListCreateEffect) {
      if (observers.TryGetValue(0, out List<IStrMutListEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnStrMutListEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<IStrMutListEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnStrMutListEffect(effect);
        }
      }
    }
    effectsStrMutListCreateEffect.Clear();

  }

    public int GetTerrainTileByLocationMutMapHash(int id, int version, TerrainTileByLocationMutMapIncarnation incarnation) {
      int result = id * version;
      foreach (var entry in incarnation.map) {
        result += id * version * entry.Key.GetDeterministicHashCode() * entry.Value.GetDeterministicHashCode();
      }
      return result;
    }
    public TerrainTileByLocationMutMapIncarnation GetTerrainTileByLocationMutMapIncarnation(int id) {
      return rootIncarnation.incarnationsTerrainTileByLocationMutMap[id].incarnation;
    }
    public TerrainTileByLocationMutMap GetTerrainTileByLocationMutMap(int id) {
      return new TerrainTileByLocationMutMap(this, id);
    }
    public List<TerrainTileByLocationMutMap> AllTerrainTileByLocationMutMap() {
      List<TerrainTileByLocationMutMap> result = new List<TerrainTileByLocationMutMap>(rootIncarnation.incarnationsTerrainTileByLocationMutMap.Count);
      foreach (var id in rootIncarnation.incarnationsTerrainTileByLocationMutMap.Keys) {
        result.Add(new TerrainTileByLocationMutMap(this, id));
      }
      return result;
    }
    public bool TerrainTileByLocationMutMapExists(int id) {
      return rootIncarnation.incarnationsTerrainTileByLocationMutMap.ContainsKey(id);
    }
    public void CheckHasTerrainTileByLocationMutMap(TerrainTileByLocationMutMap thing) {
      CheckRootsEqual(this, thing.root);
      CheckHasTerrainTileByLocationMutMap(thing.id);
    }
    public void CheckHasTerrainTileByLocationMutMap(int id) {
      if (!rootIncarnation.incarnationsTerrainTileByLocationMutMap.ContainsKey(id)) {
        throw new System.Exception("Invalid TerrainTileByLocationMutMap}: " + id);
      }
    }
    public TerrainTileByLocationMutMap EffectTerrainTileByLocationMutMapCreate() {
      CheckUnlocked();
      var id = NewId();
      EffectInternalCreateTerrainTileByLocationMutMap(
          id,
          rootIncarnation.version,
          new TerrainTileByLocationMutMapIncarnation(
              new SortedDictionary<Location, int>()));
      return new TerrainTileByLocationMutMap(this, id);
    }
       
    public void EffectInternalCreateTerrainTileByLocationMutMap(int id, int incarnationVersion, TerrainTileByLocationMutMapIncarnation incarnation) {
      var effect = new TerrainTileByLocationMutMapCreateEffect(id);
      rootIncarnation.incarnationsTerrainTileByLocationMutMap
          .Add(
              id,
              new VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>(
                  incarnationVersion,
                  incarnation));
      effectsTerrainTileByLocationMutMapCreateEffect.Add(effect);
    }
    public void EffectTerrainTileByLocationMutMapDelete(int id) {
      CheckUnlocked();
      var effect = new TerrainTileByLocationMutMapDeleteEffect(id);
      effectsTerrainTileByLocationMutMapDeleteEffect.Add(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsTerrainTileByLocationMutMap[id];
      rootIncarnation.incarnationsTerrainTileByLocationMutMap.Remove(id);
    }
    public void EffectTerrainTileByLocationMutMapAdd(int mapId, Location key, int value) {
      CheckUnlocked();
      CheckHasTerrainTileByLocationMutMap(mapId);
      CheckHasTerrainTile(value);

      var effect = new TerrainTileByLocationMutMapAddEffect(mapId, key, value);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId];
      if (oldIncarnationAndVersion.incarnation.map.ContainsKey(key)) {
        throw new Exception("Key exists! " + key);
      }
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.map.Add(key, value);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.map;
        var newMap = new SortedDictionary<Location, int>(oldMap);
        newMap.Add(key, value);
        var newIncarnation = new TerrainTileByLocationMutMapIncarnation(newMap);
        rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId] =
            new VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      effectsTerrainTileByLocationMutMapAddEffect.Add(effect);
    }
       
    public void EffectTerrainTileByLocationMutMapRemove(int mapId, Location key) {
      CheckUnlocked();
      CheckHasTerrainTileByLocationMutMap(mapId);

      var effect = new TerrainTileByLocationMutMapRemoveEffect(mapId, key);

      var oldIncarnationAndVersion = rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId];
      if (!oldIncarnationAndVersion.incarnation.map.ContainsKey(key)) {
        throw new Exception("Key doesnt exist! " + key);
      }
      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        var oldValue = oldIncarnationAndVersion.incarnation.map[key];
        oldIncarnationAndVersion.incarnation.map.Remove(key);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.map;
        var newMap = new SortedDictionary<Location, int>(oldMap);
        newMap.Remove(key);
        var newIncarnation = new TerrainTileByLocationMutMapIncarnation(newMap);
        rootIncarnation.incarnationsTerrainTileByLocationMutMap[mapId] =
            new VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>(
                rootIncarnation.version, newIncarnation);
      }
      effectsTerrainTileByLocationMutMapRemoveEffect.Add(effect);
    }
    public void AddTerrainTileByLocationMutMapObserver(int id, ITerrainTileByLocationMutMapEffectObserver observer) {
      List<ITerrainTileByLocationMutMapEffectObserver> obsies;
      if (!observersForTerrainTileByLocationMutMap.TryGetValue(id, out obsies)) {
        obsies = new List<ITerrainTileByLocationMutMapEffectObserver>();
      }
      obsies.Add(observer);
      observersForTerrainTileByLocationMutMap[id] = obsies;
    }

    public void RemoveTerrainTileByLocationMutMapObserver(int id, ITerrainTileByLocationMutMapEffectObserver observer) {
      if (observersForTerrainTileByLocationMutMap.ContainsKey(id)) {
        var map = observersForTerrainTileByLocationMutMap[id];
        map.Remove(observer);
        if (map.Count == 0) {
          observersForTerrainTileByLocationMutMap.Remove(id);
        }
      } else {
        throw new Exception("Couldnt find!");
      }
    }

  public void BroadcastTerrainTileByLocationMutMapEffects(
      SortedDictionary<int, List<ITerrainTileByLocationMutMapEffectObserver>> observers) {
    foreach (var effect in effectsTerrainTileByLocationMutMapDeleteEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileByLocationMutMapEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileByLocationMutMapEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
        observersForTerrainTileByLocationMutMap.Remove(effect.id);
      }
    }
    effectsTerrainTileByLocationMutMapDeleteEffect.Clear();

    foreach (var effect in effectsTerrainTileByLocationMutMapAddEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileByLocationMutMapEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileByLocationMutMapEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
    }
    effectsTerrainTileByLocationMutMapAddEffect.Clear();

    foreach (var effect in effectsTerrainTileByLocationMutMapRemoveEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileByLocationMutMapEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileByLocationMutMapEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
    }
    effectsTerrainTileByLocationMutMapRemoveEffect.Clear();

    foreach (var effect in effectsTerrainTileByLocationMutMapCreateEffect) {
      if (observers.TryGetValue(0, out List<ITerrainTileByLocationMutMapEffectObserver> globalObservers)) {
        foreach (var observer in globalObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
      if (observers.TryGetValue(effect.id, out List<ITerrainTileByLocationMutMapEffectObserver> objObservers)) {
        foreach (var observer in objObservers) {
          observer.OnTerrainTileByLocationMutMapEffect(effect);
        }
      }
    }
    effectsTerrainTileByLocationMutMapCreateEffect.Clear();

  }
}

}
