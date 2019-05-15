using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AverageNormals : MonoBehaviour
{

    private static Dictionary<Mesh, Mesh> m_all_meshes = new Dictionary<Mesh, Mesh>();
    //private static int stencilId = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (MeshFilter filter in GetComponentsInChildren<MeshFilter>(true))
        {
            if (filter.sharedMesh == null || !filter.sharedMesh.isReadable) continue;

            if (!m_all_meshes.ContainsKey(filter.sharedMesh))
            {
                Mesh copy = Instantiate(filter.sharedMesh);
                DoAverageNormals(copy);
                m_all_meshes[filter.sharedMesh] = copy;
            }

            filter.sharedMesh = m_all_meshes[filter.sharedMesh];
        }

        /*foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true))
        {
            renderer.material.SetFloat("_StencilId", stencilId);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private struct VertInfo
    {
        public Vector3 vert;
        public int origIndex;
        public Vector3 normal;
        public Vector3 averagedNormal;
    }

    void DoAverageNormals(Mesh mesh)
    {
        Vector3[] verts = mesh.vertices;
        Vector3[] normals = mesh.normals;
        VertInfo[] vertInfo = new VertInfo[verts.Length];
        for (int i = 0; i < verts.Length; i++)
        {
            vertInfo[i] = new VertInfo()
            {
                vert = verts[i],
                origIndex = i,
                normal = normals[i]
            };
        }
        var groups = vertInfo.GroupBy(x => x.vert);
        VertInfo[] processedVertInfo = new VertInfo[vertInfo.Length];
        int index = 0;
        foreach (IGrouping<Vector3, VertInfo> group in groups)
        {
            Vector3 avgNormal = Vector3.zero;
            foreach (VertInfo item in group)
            {
                avgNormal += item.normal;
            }
            avgNormal = avgNormal / group.Count();
            foreach (VertInfo item in group)
            {
                processedVertInfo[index] = new VertInfo()
                {
                    vert = item.vert,
                    origIndex = item.origIndex,
                    normal = item.normal,
                    averagedNormal = avgNormal
                };
                index++;
            }
        }
        Vector3[] uvs = new Vector3[verts.Length];

        for (int i = 0; i < processedVertInfo.Length; i++)
        {
            VertInfo info = processedVertInfo[i];

            int origIndex = info.origIndex;
            Vector3 normal = info.averagedNormal;

            uvs[origIndex] = new Vector3(normal.x, normal.y, normal.z);
        }

        mesh.SetUVs(1, new List<Vector3>(uvs));
    }
}
