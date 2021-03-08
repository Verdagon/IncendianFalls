using System;
using System.Collections;

using System.Collections.Generic;

namespace Geomancer.Model {
public class RootIncarnation {
  public readonly int version;
  public int nextId;
  public int hash;
  public readonly SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>> incarnationsTerrainTile;
  public readonly SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>> incarnationsTerrain;
  public readonly SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>> incarnationsLevel;
  public readonly SortedDictionary<int, VersionAndIncarnation<RandIncarnation>> incarnationsRand;
  public readonly SortedDictionary<int, VersionAndIncarnation<StrMutListIncarnation>> incarnationsStrMutList;
  public readonly SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>> incarnationsTerrainTileByLocationMutMap;
  public RootIncarnation(int version, int nextId, int hash) {
    this.version = version;
    this.nextId = nextId;
    this.hash = hash;
    this.incarnationsTerrainTile = new SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>>();
    this.incarnationsTerrain = new SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>>();
    this.incarnationsLevel = new SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>>();
    this.incarnationsRand = new SortedDictionary<int, VersionAndIncarnation<RandIncarnation>>();
    this.incarnationsStrMutList = new SortedDictionary<int, VersionAndIncarnation<StrMutListIncarnation>>();
    this.incarnationsTerrainTileByLocationMutMap = new SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>>();
  }
  public RootIncarnation(
      int newVersion,
      int newNextId,
      int newHash,
      RootIncarnation that) {
    this.version = newVersion;
    this.nextId = newNextId;
    this.hash = newHash;
    this.incarnationsTerrainTile = new SortedDictionary<int, VersionAndIncarnation<TerrainTileIncarnation>>(that.incarnationsTerrainTile);
    this.incarnationsTerrain = new SortedDictionary<int, VersionAndIncarnation<TerrainIncarnation>>(that.incarnationsTerrain);
    this.incarnationsLevel = new SortedDictionary<int, VersionAndIncarnation<LevelIncarnation>>(that.incarnationsLevel);
    this.incarnationsRand = new SortedDictionary<int, VersionAndIncarnation<RandIncarnation>>(that.incarnationsRand);
    this.incarnationsStrMutList = new SortedDictionary<int, VersionAndIncarnation<StrMutListIncarnation>>(that.incarnationsStrMutList);
    this.incarnationsTerrainTileByLocationMutMap = new SortedDictionary<int, VersionAndIncarnation<TerrainTileByLocationMutMapIncarnation>>(that.incarnationsTerrainTileByLocationMutMap);
  }
}

}
