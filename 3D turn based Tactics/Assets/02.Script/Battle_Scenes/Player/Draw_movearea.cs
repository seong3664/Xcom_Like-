using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Draw_movearea : MonoBehaviour
{

    LineRenderer Line;
    public List<A_Nodes> reachableNodes = new List<A_Nodes>();
    MoveCtrl MoveCtrl;

    public bool isDraw = false;
    Material quadMaterial;
    private Mesh quadMesh;
    A_Grid grid;
    [SerializeField] private Transform PointerUi;
    Image PointerUiImage;

    Transform highlighting;
    Transform Pointer;


    List<A_Nodes> neighbourNodes = new List<A_Nodes>();
    List<Vector3> boundaryarea = new List<Vector3>();
    List<Vector3> areaDrawdir = new List<Vector3>();
    private void Awake()
    {
        Transform Unit_Manager = GameObject.Find("Unit_Manager").GetComponent<Transform>();
        Line = Unit_Manager.GetComponent<LineRenderer>();
        MoveCtrl = Unit_Manager.GetComponent<MoveCtrl>();
        PointerUi = Unit_Manager.GetChild(0).GetChild(1).GetComponent<Transform>();
        PointerUiImage = PointerUi.GetComponent<Image>();
        grid = GameObject.Find("Grid_Manager").GetComponent<A_Grid>();
        highlighting = Unit_Manager.GetChild(0).GetChild(2).GetComponent<Transform>();
        Pointer = Unit_Manager.GetChild(0).GetChild(1).GetComponent<Transform>();

    }
    private void Start()
    {
        //quadMesh = CreateQuad(1f); // ���� �޽� ����
        Line.enabled = false;
        quadMaterial = Resources.Load("Boundary") as Material;
    }
    void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }
    void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }
    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        OnPostRender();
    }
    /// <summary>
    /// ��� ���� ���� ������ �׸��� �ż���
    /// </summary>
    /// <param name="StartPos"></param>
    /// <param name="Nodepath"></param>
    public void Draw_MoveLine(Vector3 StartPos, List<A_Nodes> Nodepath)
    {

        if (Nodepath != null && Nodepath.Count >= 1)
        {
            PointerUiImage.enabled = true;
            highlighting.gameObject.SetActive(true);
            Line.enabled = true;
            Line.positionCount = Nodepath.Count + 1;
            Line.SetPosition(0, StartPos + Vector3.up * 0.05f); // ���� ��ġ ����

            for (int i = 0; i < Nodepath.Count; i++)
            {
                Line.SetPosition(i + 1, Nodepath[i].WorldPos + Vector3.up * 0.05f); // ���� ��ġ ����
            }
            PointerUi.position = Nodepath[Nodepath.Count - 1].WorldPos + Vector3.up * 0.01f;
            highlighting.position = Nodepath[Nodepath.Count - 1].WorldPos + Vector3.up * 0.01f;
            highlighting.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
        else
        {
            Line.enabled = false;
            PointerUiImage.enabled = false;
            highlighting.gameObject.SetActive(false);
        }
    }

    public void boundarydraw()
    {
        boundaryarea.Clear();
        areaDrawdir.Clear();
        foreach (A_Nodes node in reachableNodes)
        {
            neighbourNodes = grid.GetNeighbours(node, false);
            foreach (A_Nodes neighbour in neighbourNodes)
            {
                if (!reachableNodes.Contains(neighbour))
                {
                    boundaryarea.Add(node.WorldPos);
                    areaDrawdir.Add((node.WorldPos - neighbour.WorldPos));
                }
            }
        }
    }

    void OnPostRender()
    {
        if (isDraw)
        {
            if (boundaryarea != null)
            {
                GL.PushMatrix();
                GL.LoadIdentity();
                GL.LoadProjectionMatrix(Camera.main.projectionMatrix);
                GL.MultMatrix(Camera.main.worldToCameraMatrix);
                quadMaterial.SetPass(0); // ����� ��Ƽ���� ����
                quadMaterial.color = Color.blue;
                //GL.Color(Color.blue);
                GL.Begin(GL.LINES);

                for (int i = 0; i < areaDrawdir.Count; i++)
                {
                    if (Mathf.Abs(areaDrawdir[i].x) > 0)
                    {
                        Vector3 start = boundaryarea[i] - areaDrawdir[i].normalized + Vector3.up * 0.1f - Vector3.forward;
                        Vector3 end = boundaryarea[i] - areaDrawdir[i].normalized + Vector3.up * 0.1f + Vector3.forward;
                        GL.Vertex(start);
                        GL.Vertex(end);
                    }
                    if (Mathf.Abs(areaDrawdir[i].z) > 0)
                    {
                        Vector3 start = boundaryarea[i] - areaDrawdir[i].normalized + Vector3.up * 0.1f - Vector3.right;
                        Vector3 end = boundaryarea[i] - areaDrawdir[i].normalized + Vector3.up * 0.01f + Vector3.right;
                        GL.Vertex(start);
                        GL.Vertex(end);
                    }

                }

                GL.End();
                GL.PopMatrix();
            }
        }
        else
        {
            Line.enabled = false;
            PointerUiImage.enabled = false;
            highlighting.gameObject.SetActive(false);
        }
    }





}