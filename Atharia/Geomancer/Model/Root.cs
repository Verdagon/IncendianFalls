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

public delegate void IEffectObserver(IEffect effect);

public interface IEffect {
  void visitIEffect(IEffectVisitor visitor);
}

public interface IEffectVisitor {
void visitTerrainTileEffect(ITerrainTileEffect effect);
void visitTerrainEffect(ITerrainEffect effect);
void visitLevelEffect(ILevelEffect effect);
void visitRandEffect(IRandEffect effect);
void visitStrMutListEffect(IStrMutListEffect effect);
void visitTerrainTileByLocationMutMapEffect(ITerrainTileByLocationMutMapEffect effect);

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

  public List<IEffectObserver> effectObservers;

  // This *always* points to a live RootIncarnation. When we snapshot, we eagerly
  // make a new one of these.
  private RootIncarnation rootIncarnation;

  bool locked;

  // 0 means everything
  public Root(ILogger logger) {
    this.logger = logger;
    this.effectObservers = new List<IEffectObserver>();
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

  public void AddObserver(IEffectObserver obs) {
    effectObservers.Add(obs);
  }

  public void RemoveObserver(IEffectObserver obs) {
    effectObservers.Remove(obs);
  }

  private void NotifyEffect(IEffect effect) {
    foreach (var obs in effectObservers) {
      obs(effect);
    }
  }

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

  public (List<IEffect>, T) Transact<T>(ITransaction<T> transaction) {
    var stopwatch = new System.Diagnostics.Stopwatch();
    stopwatch.Start();

    if (!locked) {
      throw new Exception("Can't unlock, not locked!");
    }
    locked = false;
    // var rollbackPoint = Snapshot();

    var effects = new List<IEffect>();
    IEffectObserver effectObserver = (effect) => effects.Add(effect);
    AddObserver(effectObserver);

    try {
      var result = transaction();
      return (effects, result);
    } catch (Exception e) {
      // logger.Error("Rolling back because of error: " + e.Message + "\n" + e.StackTrace);
      // Revert(rollbackPoint);
      logger.Error("Encountered error in transaction: " + e.Message + "\n" + e.StackTrace);
      throw;
    } finally {
      RemoveObserver(effectObserver);
      if (locked) {
        logger.Error("Can't lock, already locked!");
        Environment.Exit(1);
      }
      locked = true;
      // CheckForViolations();

      stopwatch.Stop();
      var calculationDuration = stopwatch.Elapsed.TotalMilliseconds;

      logger.Info("Transaction run time " + calculationDuration);
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
            for (int i = 0; i < sourceObjIncarnation.list.Count; i++) {
              EffectStrMutListAdd(objId, i, sourceObjIncarnation.list[i]);
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
    CheckHasTerrainTile(id);
    return new TerrainTile(this, id);
  }
  public TerrainTile GetTerrainTileOrNull(int id) {
    if (TerrainTileExists(id)) {
      return new TerrainTile(this, id);
    } else {
      return new TerrainTile(this, 0);
    }
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
    var effect = new TerrainTileCreateEffect(id, incarnation.Copy());
    rootIncarnation.incarnationsTerrainTile.Add(
        id,
        new VersionAndIncarnation<TerrainTileIncarnation>(
            incarnationVersion,
            incarnation));
    NotifyEffect(effect);
  }

  public void EffectTerrainTileDelete(int id) {
    CheckUnlocked();
    var effect = new TerrainTileDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsTerrainTile[id];

    rootIncarnation.incarnationsTerrainTile.Remove(id);
    NotifyEffect(effect);
  }

     
  public int GetTerrainTileHash(int id, int version, TerrainTileIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.elevation.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.members.GetDeterministicHashCode();
    return result;
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

    NotifyEffect(effect);
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
    CheckHasTerrain(id);
    return new Terrain(this, id);
  }
  public Terrain GetTerrainOrNull(int id) {
    if (TerrainExists(id)) {
      return new Terrain(this, id);
    } else {
      return new Terrain(this, 0);
    }
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
    var effect = new TerrainCreateEffect(id, incarnation.Copy());
    rootIncarnation.incarnationsTerrain.Add(
        id,
        new VersionAndIncarnation<TerrainIncarnation>(
            incarnationVersion,
            incarnation));
    NotifyEffect(effect);
  }

  public void EffectTerrainDelete(int id) {
    CheckUnlocked();
    var effect = new TerrainDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsTerrain[id];

    rootIncarnation.incarnationsTerrain.Remove(id);
    NotifyEffect(effect);
  }

     
  public int GetTerrainHash(int id, int version, TerrainIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.pattern.GetDeterministicHashCode();
    result += id * version * 2 * incarnation.elevationStepHeight.GetDeterministicHashCode();
    result += id * version * 3 * incarnation.tiles.GetDeterministicHashCode();
    return result;
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
    CheckHasLevel(id);
    return new Level(this, id);
  }
  public Level GetLevelOrNull(int id) {
    if (LevelExists(id)) {
      return new Level(this, id);
    } else {
      return new Level(this, 0);
    }
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
    var effect = new LevelCreateEffect(id, incarnation.Copy());
    rootIncarnation.incarnationsLevel.Add(
        id,
        new VersionAndIncarnation<LevelIncarnation>(
            incarnationVersion,
            incarnation));
    NotifyEffect(effect);
  }

  public void EffectLevelDelete(int id) {
    CheckUnlocked();
    var effect = new LevelDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsLevel[id];

    rootIncarnation.incarnationsLevel.Remove(id);
    NotifyEffect(effect);
  }

     
  public int GetLevelHash(int id, int version, LevelIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.terrain.GetDeterministicHashCode();
    return result;
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
    CheckHasRand(id);
    return new Rand(this, id);
  }
  public Rand GetRandOrNull(int id) {
    if (RandExists(id)) {
      return new Rand(this, id);
    } else {
      return new Rand(this, 0);
    }
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
    var effect = new RandCreateEffect(id, incarnation.Copy());
    rootIncarnation.incarnationsRand.Add(
        id,
        new VersionAndIncarnation<RandIncarnation>(
            incarnationVersion,
            incarnation));
    NotifyEffect(effect);
  }

  public void EffectRandDelete(int id) {
    CheckUnlocked();
    var effect = new RandDeleteEffect(id);

    var oldIncarnationAndVersion =
        rootIncarnation.incarnationsRand[id];

    rootIncarnation.incarnationsRand.Remove(id);
    NotifyEffect(effect);
  }

     
  public int GetRandHash(int id, int version, RandIncarnation incarnation) {
    int result = id * version;
    result += id * version * 1 * incarnation.rand.GetDeterministicHashCode();
    return result;
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

    NotifyEffect(effect);
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
      CheckHasStrMutList(id);
      return new StrMutList(this, id);
    }
    public StrMutList GetStrMutListOrNull(int id) {
      if (StrMutListExists(id)) {
        return new StrMutList(this, id);
      } else {
        return new StrMutList(this, 0);
      }
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
      Asserts.Assert(!rootIncarnation.incarnationsStrMutList.ContainsKey(id));
      EffectInternalCreateStrMutList(id, rootIncarnation.version, new StrMutListIncarnation(new List<string>()));
      return new StrMutList(this, id);
    }
    public StrMutList EffectStrMutListCreate(IEnumerable<string> elements) {
      var list = EffectStrMutListCreate();
      foreach (var element in elements) {
        list.Add(element);
      }
      return list;
    }
    public void EffectInternalCreateStrMutList(int id, int incarnationVersion, StrMutListIncarnation incarnation) {
      var effect = new StrMutListCreateEffect(id);
      rootIncarnation.incarnationsStrMutList
          .Add(
              id,
              new VersionAndIncarnation<StrMutListIncarnation>(
                  incarnationVersion,
                  incarnation));
      NotifyEffect(effect);
    }
    public void EffectStrMutListDelete(int id) {
      CheckUnlocked();
      var effect = new StrMutListDeleteEffect(id);
      NotifyEffect(effect);
      var versionAndIncarnation = rootIncarnation.incarnationsStrMutList[id];
      rootIncarnation.incarnationsStrMutList.Remove(id);
    }
    public void EffectStrMutListAdd(int listId, int addIndex, string element) {
      CheckUnlocked();
      CheckHasStrMutList(listId);

    

      var oldIncarnationAndVersion = rootIncarnation.incarnationsStrMutList[listId];
      var effect = new StrMutListAddEffect(listId, addIndex, element);

      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
        oldIncarnationAndVersion.incarnation.list.Insert(addIndex, element);
      } else {
        var oldMap = oldIncarnationAndVersion.incarnation.list;
        var newMap = new List<string>(oldMap);
        newMap.Insert(addIndex, element);
        var newIncarnation = new StrMutListIncarnation(newMap);
        rootIncarnation.incarnationsStrMutList[listId] =
            new VersionAndIncarnation<StrMutListIncarnation>(
                rootIncarnation.version,
                newIncarnation);
      }
      NotifyEffect(effect);
    }
    public void EffectStrMutListRemoveAt(int listId, int index) {
      CheckUnlocked();
      CheckHasStrMutList(listId);

      var effect = new StrMutListRemoveEffect(listId, index);


      var oldIncarnationAndVersion = rootIncarnation.incarnationsStrMutList[listId];
      // Check that its there
      var oldElement = oldIncarnationAndVersion.incarnation.list[index];

      if (oldIncarnationAndVersion.version == rootIncarnation.version) {
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
      NotifyEffect(effect);
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
      Asserts.Assert(!rootIncarnation.incarnationsTerrainTileByLocationMutMap.ContainsKey(id));
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
      NotifyEffect(effect);
    }
    public void EffectTerrainTileByLocationMutMapDelete(int id) {
      CheckUnlocked();
      var effect = new TerrainTileByLocationMutMapDeleteEffect(id);
      NotifyEffect(effect);
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
      NotifyEffect(effect);
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
      NotifyEffect(effect);
    }
}

}
