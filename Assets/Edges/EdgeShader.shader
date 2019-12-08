
Shader "Unlit/EdgeShader"
{
  Properties
  {
    _OutlineRadius ("Outline Radius", Float) = 0.025
  }
  SubShader
  {
    Tags { "RenderType"="Opaque" }
    LOD 100

    Pass
    {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma geometry geom
      // make fog work
      #pragma multi_compile_fog

      #include "UnityCG.cginc"
      
      float _OutlineRadius;
      
      struct appdata
      {
        float4 vertex : POSITION;
        float4 normal : NORMAL;
      };

      struct v2g
      {
        float4 vertex : SV_POSITION;
        float4 normal : NORMAL;
      };
      
      struct g2f
      {
        UNITY_FOG_COORDS(1)
        float4 vertex : SV_POSITION;
        float4 normal : NORMAL;
      };
      
      v2g vert (appdata v) {
        v2g o;
        o.vertex = v.vertex;
        o.normal = v.normal;
        return o;
      }
      
      struct CylinderCoordSpace {
        float3 pAxisInWorld;
        float3 qAxisInWorld;
        float3 rAxisInWorld;
        float3 originInWorld;
      };
      
      float3 pqrToXyz(
          CylinderCoordSpace cylSpace,
          float3 pqr) {
        return pqr.x * cylSpace.pAxisInWorld +
            pqr.y * cylSpace.qAxisInWorld +
            pqr.z * cylSpace.rAxisInWorld +
            cylSpace.originInWorld;
      }
      
      float3 pqrToXyzForNormal(
          CylinderCoordSpace cylSpace,
          float3 pqr) {
        return pqr.x * cylSpace.pAxisInWorld +
            pqr.y * cylSpace.qAxisInWorld +
            pqr.z * cylSpace.rAxisInWorld;
      }
      
      void emitVertex(inout TriangleStream<g2f> outputStream, float3 position, float3 normal) {
        g2f aVertex;
        aVertex.vertex = mul(UNITY_MATRIX_VP, float4(position, 1));
        aVertex.normal = float4(normal, 1);
        UNITY_TRANSFER_FOG(aVertex,aVertex.vertex);
        outputStream.Append(aVertex);
      }
      
      float3 arbitraryPerpendicular(float3 v) {
        float3 c = float3(1, 0, 0);
        if (v.y == 0 && v.z == 0)
          c = float3(0, 1, 0);
        return cross(v, c);
      }
      
      // If this number is too low, then the later vertices will just disappear
      // and make things look weird.
      [maxvertexcount(70)]
      void geom(line v2g i[2], inout TriangleStream<g2f> outputStream) {
        // Anything ending in XYZ is in world space.
        float3 segmentOriginXYZ = mul(unity_ObjectToWorld, i[0].vertex);
        float3 segmentDestinationXYZ = mul(unity_ObjectToWorld, i[1].vertex);
        float segmentLength = distance(segmentOriginXYZ, segmentDestinationXYZ);
        float3 segmentDirection = normalize(segmentDestinationXYZ - segmentOriginXYZ);
        float3 arbitraryOtherDirection = normalize(arbitraryPerpendicular(segmentDirection));
        float3 perpendicularToSegment = normalize(cross(segmentDirection, arbitraryOtherDirection));
        
        // The segment's direction shall henceforthwith be known as 'r'.
        float3 rAxisInWorld = segmentDirection;
        // The arbitrary perpendicular direction shall be known as 'p'.
        float3 pAxisInWorld = arbitraryOtherDirection;
        // Whatever's perpendicular to r and p is 'q'.
        float3 qAxisInWorld = perpendicularToSegment;
        CylinderCoordSpace cylSpace = { pAxisInWorld, qAxisInWorld, rAxisInWorld, segmentOriginXYZ };
        // Now that we have p, q, and r, we can make a cylinder in that space.
        // If you imagine the cylinder is standing upright, 'r' is up.
        
        
        float radius = _OutlineRadius;
        
        // Since we can't make a round cylinder, we have to divide it into
        // multiple sides. If it had 6 sides it would be like a hexagon if you
        // look straight on, if it had 4 sides it would be a prismoid, and so
        // on. We're going with 7 for now, 7 "wedges".
        int numWedges = 7;
        // The first wedge goes from 0 to 2pi*1/7, the second wedge goes from
        // 2pi*1/7 to 2pi*2/7, and so on. The go from their wedge begin angle,
        // aka their "dawn", to their wedge end angle, aka their "dusk".
        
        // The cylinder is not just a cylinder, it's got a hemisphere on each
        // end.
        // The hemisphere is made of multiple levels. 1 level would be a cone,
        // and 10 levels would be super round. We're going with 2 here.
        // The end level is the "head", and the other level is the "shoulder".
        
        // So, the wedge is made of five parts:
        // - origin hand
        // - origin arm
        // - chest
        // - destination arm
        // - destination hand
        // And the boundaries between them are:
        // - origin wrist
        // - origin shoulder
        // - destination shoulder
        // - destination wrist
        
        // To do this, we need a triangle strip with these points per wedge:
        // - origin tip
        // - origin wrist dawn
        // - origin wrist dusk
        // - origin shoulder dawn
        // - origin shoulder dusk
        // - destination shoulder dawn
        // - destination shoulder dusk
        // - destination wrist dawn
        // - destination wrist dusk
        // - destination tip
        
        // The below code actually flips the order of dawn and dusk points,
        // because for some reason triangle strips only work like that.
        // Apparently the order of the first three vertices affect the entire
        // strip.
        
        for (int dawnI = 0; dawnI < numWedges; dawnI++) {
          float dawn = 2 * 3.14159f * (float)dawnI / numWedges;
          float dusk = 2 * 3.14159f * (float)(dawnI + 1) / numWedges;
          
          float3 originTipPQR = float3(0, 0, -radius);
          float3 originTipXYZ = pqrToXyz(cylSpace, originTipPQR);
          float3 originTipNormalXYZ = pqrToXyzForNormal(cylSpace, originTipPQR);
            
          emitVertex(outputStream, originTipXYZ, originTipNormalXYZ);
          
          float3 originWristDuskPQR = float3(0.707f * radius * cos(dusk), 0.707f * radius * sin(dusk), -0.707f * radius);
          float3 originWristDuskXYZ = pqrToXyz(cylSpace, originWristDuskPQR);
          float3 originWristDuskNormalXYZ = pqrToXyzForNormal(cylSpace, originWristDuskPQR);
          
          emitVertex(outputStream, originWristDuskXYZ, originWristDuskNormalXYZ);
          
          float3 originWristDawnPQR = float3(0.707f * radius * cos(dawn), 0.707f * radius * sin(dawn), -0.707f * radius);
          float3 originWristDawnXYZ = pqrToXyz(cylSpace, originWristDawnPQR);
          float3 originWristDawnNormalXYZ = pqrToXyzForNormal(cylSpace, originWristDawnPQR);
          
          emitVertex(outputStream, originWristDawnXYZ, originWristDawnNormalXYZ);
          
          float3 originShoulderDuskPQR = float3(radius * cos(dusk), radius * sin(dusk), 0);
          float3 originShoulderDuskXYZ = pqrToXyz(cylSpace, originShoulderDuskPQR);
          float3 originShoulderDuskNormalXYZ = pqrToXyzForNormal(cylSpace, originShoulderDuskPQR);
          
          emitVertex(outputStream, originShoulderDuskXYZ, originShoulderDuskNormalXYZ);
          
          float3 originShoulderDawnPQR = float3(radius * cos(dawn), radius * sin(dawn), 0);
          float3 originShoulderDawnXYZ = pqrToXyz(cylSpace, originShoulderDawnPQR);
          float3 originShoulderDawnNormalXYZ = pqrToXyzForNormal(cylSpace, originShoulderDawnPQR);
          
          emitVertex(outputStream, originShoulderDawnXYZ, originShoulderDawnNormalXYZ);
          
          float3 destinationShoulderDuskPQR = float3(radius * cos(dusk), radius * sin(dusk), segmentLength);
          float3 destinationShoulderDuskXYZ = pqrToXyz(cylSpace, destinationShoulderDuskPQR);
          float3 destinationShoulderDuskNormalXYZ = pqrToXyzForNormal(cylSpace, destinationShoulderDuskPQR);
          
          emitVertex(outputStream, destinationShoulderDuskXYZ, destinationShoulderDuskNormalXYZ);
          
          float3 destinationShoulderDawnPQR = float3(radius * cos(dawn), radius * sin(dawn), segmentLength);
          float3 destinationShoulderDawnXYZ = pqrToXyz(cylSpace, destinationShoulderDawnPQR);
          float3 destinationShoulderDawnNormalXYZ = pqrToXyzForNormal(cylSpace, destinationShoulderDawnPQR);
          
          emitVertex(outputStream, destinationShoulderDawnXYZ, destinationShoulderDawnNormalXYZ);
          
          float3 destinationWristDuskPQR = float3(0.707f * radius * cos(dusk), 0.707f * radius * sin(dusk), segmentLength + 0.707f * radius);
          float3 destinationWristDuskXYZ = pqrToXyz(cylSpace, destinationWristDuskPQR);
          float3 destinationWristDuskNormalXYZ = pqrToXyzForNormal(cylSpace, destinationWristDuskPQR);
          
          emitVertex(outputStream, destinationWristDuskXYZ, destinationWristDuskNormalXYZ);
          
          float3 destinationWristDawnPQR = float3(0.707f * radius * cos(dawn), 0.707f * radius * sin(dawn), segmentLength + 0.707f * radius);
          float3 destinationWristDawnXYZ = pqrToXyz(cylSpace, destinationWristDawnPQR);
          float3 destinationWristDawnNormalXYZ = pqrToXyzForNormal(cylSpace, destinationWristDawnPQR);
          
          emitVertex(outputStream, destinationWristDawnXYZ, destinationWristDawnNormalXYZ);
          
          float3 destinationTipPQR = float3(0, 0, segmentLength + radius);
          float3 destinationTipXYZ = pqrToXyz(cylSpace, destinationTipPQR);
          float3 destinationTipNormalXYZ = pqrToXyzForNormal(cylSpace, destinationTipPQR);
        
          emitVertex(outputStream, destinationTipXYZ, destinationTipNormalXYZ);
          
          outputStream.RestartStrip();
        }
      }
      
      fixed4 frag (g2f i) : SV_Target {
        //float4 col = float4(.5, .5, .5, 1) + i.normal * .5f;
        float4 col = float4(0, 0, 0, 1);
        UNITY_APPLY_FOG(i.fogCoord, col);
        return col;
      }
      ENDCG
    }
  }
}
