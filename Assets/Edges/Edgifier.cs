using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edgifier {
  private struct CanonicalVertexPair {
    private int vertexA, vertexB;

    public CanonicalVertexPair(int vertexA, int vertexB) {
      this.vertexA = System.Math.Min(vertexA, vertexB);
      this.vertexB = System.Math.Max(vertexA, vertexB);
    }

    public override string ToString() {
      return "CanonicalVertexPair(" + vertexA + ", " + vertexB + ")";
    }
  }

  private struct VertexPair {
    public int vertexA, vertexB;

    public VertexPair(int vertexA, int vertexB) {
      this.vertexA = System.Math.Min(vertexA, vertexB);
      this.vertexB = System.Math.Max(vertexA, vertexB);
    }
  }

  private struct Edge {
    int edgeId;
    int vertexA, vertexB;
    int triangleA;
    int sideInTriangleA;
    int triangleB;
    int sideInTriangleB;
  }

  private struct TriangleSide {
    public int triangleIndex;
    public int sideIndex;
    public int vertexAIndex, vertexBIndex;

    public TriangleSide(int triangleIndex, int sideIndex, int vertexAIndex, int vertexBIndex) {
      this.triangleIndex = triangleIndex;
      this.sideIndex = sideIndex;
      this.vertexAIndex = vertexAIndex;
      this.vertexBIndex = vertexBIndex;
    }
  }

  private class NeighborFinder {
    private Dictionary<int, int> canonicalVertexIndexByVertexIndex;
    private Dictionary<CanonicalVertexPair, List<TriangleSide>> triangleIndicesByCanonicalVertexPair;

    public NeighborFinder(Dictionary<int, int> canonicalVertexIndexByVertexIndex) {
      this.canonicalVertexIndexByVertexIndex = canonicalVertexIndexByVertexIndex;
      triangleIndicesByCanonicalVertexPair = new Dictionary<CanonicalVertexPair, List<TriangleSide>>();
    }

    public void add(int vertexAIndex, int vertexBIndex, int triangleIndex, int sideIndex) {
      var vertexPair = new CanonicalVertexPair(canonicalVertexIndexByVertexIndex[vertexAIndex], canonicalVertexIndexByVertexIndex[vertexBIndex]);
      if (!triangleIndicesByCanonicalVertexPair.ContainsKey(vertexPair)) {
        triangleIndicesByCanonicalVertexPair.Add(vertexPair, new List<TriangleSide>());
      }
      triangleIndicesByCanonicalVertexPair[vertexPair].Add(new TriangleSide(triangleIndex, sideIndex, vertexAIndex, vertexBIndex));
    }

    public Dictionary<CanonicalVertexPair, List<TriangleSide>> GetTriangleIndicesByCanonicalVertexPair() {
      return new Dictionary<CanonicalVertexPair, List<TriangleSide>>(triangleIndicesByCanonicalVertexPair);
    }
  }

  float getVertexEpsilon(Mesh mesh) {
    var minX = mesh.vertices[0].x;
    var maxX = mesh.vertices[0].x;
    var minY = mesh.vertices[0].y;
    var maxY = mesh.vertices[0].y;
    var minZ = mesh.vertices[0].z;
    var maxZ = mesh.vertices[0].z;

    foreach (var vertex in mesh.vertices) {
      minX = System.Math.Min(minX, vertex.x);
      maxX = System.Math.Max(maxX, vertex.x);
      minY = System.Math.Min(minY, vertex.y);
      maxY = System.Math.Max(maxY, vertex.y);
      minZ = System.Math.Min(minZ, vertex.z);
      maxZ = System.Math.Max(maxZ, vertex.z);
    }

    var spanX = maxX - minX;
    var spanY = maxY - minY;
    var spanZ = maxZ - minZ;

    var minSpan = System.Math.Min(System.Math.Min(spanX, spanY), spanZ);
    return minSpan / 1000;
  }

  bool VectorsEqual(Vector3 a, Vector3 b, float epsilon) {
    return System.Math.Abs(a.x - b.x) < epsilon &&
            System.Math.Abs(a.y - b.y) < epsilon &&
            System.Math.Abs(a.z - b.z) < epsilon;
  }

  string VectorToString(Vector3 v) {
    return "(" + v.x + ", " + v.y + ", " + v.z + ")";
  }

  List<VertexPair> CalculateEdges(Mesh mesh, float vertexEpsilon, float minDegreesBetweenNormalsToBeAnEdge) {
    // The triangles, indices, etc. in the model file aren't necessarily what's in the Unity
    // mesh, because Unity does some simplification/unsimplification/optimizing. See
    // https://gamedev.stackexchange.com/questions/66401/how-does-unity3d-lower-an-imported-obj-vertex-count
    // for more info. For us, it means that sometimes vertices are duplicated.

    // If two vertexes are at the same place, we choose one to be the canonical one (the one
    // with the lower index, actually). The canonical one will be the value and the duplicate
    // one will be the key.
    Dictionary<int, int> canonicalVertexIndexByVertexIndex = new Dictionary<int, int>();
    for (int undedupedVertexIndex = 0; undedupedVertexIndex < mesh.vertices.Length; undedupedVertexIndex++) {
      var vertexA = mesh.vertices[undedupedVertexIndex];
      for (int canonicalVertexIndex = 0; canonicalVertexIndex < mesh.vertices.Length; canonicalVertexIndex++) {
        var vertexB = mesh.vertices[canonicalVertexIndex];
        if (VectorsEqual(vertexA, vertexB, vertexEpsilon)) {
          canonicalVertexIndexByVertexIndex.Add(undedupedVertexIndex, canonicalVertexIndex);
          // We've found this one's canonical vertex so we can break out of this inner loop.
          break;
        }
      }
    }
    // There should be an entry for every vertex; if a vertex doesn't share a location with any
    // others then it should be its own canonical vertex.
    if (canonicalVertexIndexByVertexIndex.Count != mesh.vertices.Length) {
      throw new System.Exception("blaaarg");
    }

    var neighborFinder = new NeighborFinder(canonicalVertexIndexByVertexIndex);

    for (int triangleVertexAIndex = 0; triangleVertexAIndex < mesh.triangles.Length; triangleVertexAIndex += 3) {
      int triangleVertexBIndex = triangleVertexAIndex + 1;
      int triangleVertexCIndex = triangleVertexAIndex + 2;
      int vertexAIndex = mesh.triangles[triangleVertexAIndex];
      int vertexBIndex = mesh.triangles[triangleVertexBIndex];
      int vertexCIndex = mesh.triangles[triangleVertexCIndex];
      int triangleIndex = triangleVertexAIndex / 3;
      neighborFinder.add(vertexAIndex, vertexBIndex, triangleIndex, 0);
      neighborFinder.add(vertexBIndex, vertexCIndex, triangleIndex, 1);
      neighborFinder.add(vertexCIndex, vertexAIndex, triangleIndex, 2);
    }

    List<VertexPair> edges = new List<VertexPair>();

    var neighboringTriangleIndicesByVertexPair =
        neighborFinder.GetTriangleIndicesByCanonicalVertexPair();
    foreach (var vertexPairAndNeighboringTriangleIndices in neighboringTriangleIndicesByVertexPair) {
      if (vertexPairAndNeighboringTriangleIndices.Value.Count != 2) {
        Debug.Log("nyehhhh " + vertexPairAndNeighboringTriangleIndices.Key + " " + vertexPairAndNeighboringTriangleIndices.Value.Count);
        foreach (var thing in vertexPairAndNeighboringTriangleIndices.Value) {
          Debug.Log("- " + thing.triangleIndex + " " + thing.sideIndex + " " + thing.vertexAIndex + " " + mesh.vertices[thing.vertexAIndex] + " " + thing.vertexBIndex + " " + mesh.vertices[thing.vertexBIndex]);
        }
        continue;
      }
      var sideATriangle = vertexPairAndNeighboringTriangleIndices.Value[0];
      var sideAVertexAIndex = sideATriangle.vertexAIndex;
      var sideAVertexA = mesh.vertices[sideAVertexAIndex];
      var sideAVertexBIndex = sideATriangle.vertexBIndex;
      var sideAVertexB = mesh.vertices[sideAVertexBIndex];
      var sideBTriangle = vertexPairAndNeighboringTriangleIndices.Value[1];
      var sideBVertexAIndex = sideBTriangle.vertexAIndex;
      var sideBVertexA = mesh.vertices[sideBVertexAIndex];
      var sideBVertexBIndex = sideBTriangle.vertexBIndex;
      var sideBVertexB = mesh.vertices[sideBVertexBIndex];

      if (VectorsEqual(sideAVertexA, sideBVertexA, vertexEpsilon)) {
        // Do nothing
      } else if (VectorsEqual(sideAVertexA, sideBVertexB, vertexEpsilon)) {
        // Swap so A = A and B = B
        var temp = sideBVertexA;
        sideBVertexA = sideBVertexB;
        sideBVertexB = temp;
        var temp2 = sideBVertexAIndex;
        sideBVertexAIndex = sideBVertexBIndex;
        sideBVertexBIndex = temp2;
      } else {
        throw new System.Exception("blarg!");
      }

      var sideAVertexANormal = Vector3.Normalize(mesh.normals[sideAVertexAIndex]);
      var sideAVertexBNormal = Vector3.Normalize(mesh.normals[sideAVertexBIndex]);
      var sideBVertexANormal = Vector3.Normalize(mesh.normals[sideBVertexAIndex]);
      var sideBVertexBNormal = Vector3.Normalize(mesh.normals[sideBVertexBIndex]);

      // One triangle should have the same normals for all its vertices... for now. If we wanted
      // to handle other kinds of models (spheres maybe?) then we would have to figure out something
      // else here.
      if (!VectorsEqual(sideAVertexANormal, sideAVertexBNormal, vertexEpsilon)) {
        throw new System.Exception("what");
      }
      var sideANormal = sideAVertexANormal;
      if (!VectorsEqual(sideBVertexANormal, sideBVertexBNormal, vertexEpsilon)) {
        throw new System.Exception("what");
      }
      var sideBNormal = sideBVertexANormal;

      var angleBetween =
          Mathf.Rad2Deg *
              Mathf.Acos(
                Mathf.Clamp(
                  Vector3.Dot(sideAVertexANormal, sideBVertexANormal),
                  -1f,
                  1f));
      if (angleBetween >= minDegreesBetweenNormalsToBeAnEdge) {
        // Can add either side's vertices, but let's just choose A arbitrarily.
        edges.Add(new VertexPair(sideAVertexAIndex, sideAVertexBIndex));
      }
    }

    return edges;
  }

  public Mesh makeEdgeMesh(Mesh originalMesh, float vertexEpsilon, float minDegreesBetweenNormalsToBeAnEdge) {
    var vertexPairs = CalculateEdges(originalMesh, vertexEpsilon, minDegreesBetweenNormalsToBeAnEdge);

    int[] indices = new int[vertexPairs.Count * 2];
    int currentIndex = 0;
    foreach (var vertexPair in vertexPairs) {
      indices[currentIndex++] = vertexPair.vertexA;
      indices[currentIndex++] = vertexPair.vertexB;
    }

    Mesh mesh = new Mesh();
    mesh.vertices = originalMesh.vertices;
    mesh.SetIndices(indices, MeshTopology.Lines, 0);
    return mesh;
  }
}
