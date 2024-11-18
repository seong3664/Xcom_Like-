using States;
using System.Collections.Generic;
using UnityEngine;

public class A_Pathfinding : MonoBehaviour
{
    public A_Grid grid;
    
    HashSet<A_Nodes> closedSet = new HashSet<A_Nodes>();
    private void Start()
    {
        grid = GetComponent<A_Grid>();
    }

    /// <summary>
    /// ��� Ž�� �ż���
    /// MaxMovepointretrun�� true��� ��ǥ ��ġ�� �̵� ������
    /// ��� �� ���� ����� �������� ��θ� ��ȯ��.
    /// </summary>
    /// <param name="startpos"></param>
    /// <param name="targetpos"></param>
    /// <param name="maxActionPoints"></param>
    /// <returns></returns>
    public List<A_Nodes> A_StarAlgorithm(Vector3 startpos,Vector3 targetpos ,float maxActionPoints,bool MaxMovepointretrun)
     {
        A_Nodes startNode = grid.GetNodeFromWorldPoint(startpos);
        A_Nodes targetNode = grid.GetNodeFromWorldPoint(targetpos);
        // SortedSet을 사용하고, 우선순위를 gCost + hCost로 비교하눈 Comparer를 생성
        SortedSet<A_Nodes> openSet = new SortedSet<A_Nodes>(new NodeComparer());
        closedSet.Clear();

            // 노드를 OpenSet에 추가하고 
            openSet.Add(startNode);
            startNode.gCost = 0;
            startNode.hCost = GetDistance(startNode, targetNode);
            //openset을 다탐색하거나, 목표 위치까지의 경로를 탐색할 때까지 반복
            while (openSet.Count > 0)
            {
                // fcost가 가장 작은 노드부터 시작
                A_Nodes currentNode = openSet.Min;

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    return RetracePath(startNode, targetNode);
                 
                }

                foreach (A_Nodes neighbour in grid.GetNeighbours(currentNode,true))
                {
                    if (!neighbour.Iswalkable || closedSet.Contains(neighbour))
                        continue;

                    float moveCost = GetMoveCost(currentNode, neighbour);
                    float newCostToNeighbour = currentNode.gCost + moveCost;
                    //노드로 이동하기 위한 이동력이 충분한지(g코스트는 여태까지 소모한 값 거기에 다음 노드로 이동하기 위한 코스트 +)
                    if (newCostToNeighbour <= maxActionPoints && (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)))
                    {
                        //행동력이 충분하다면
                        if (openSet.Contains(neighbour))
                        {
                            openSet.Remove(neighbour);
                        }

                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parentNode = currentNode;

                        openSet.Add(neighbour);
                    }
                    //아닐시 while문이 다시 돈다. 마지막 노드일 경우 null
                if (newCostToNeighbour > maxActionPoints && MaxMovepointretrun == true)
                {//하지만 맥스포인트리턴이 트루라면, 목표 위치가 이동력을 벗어났더라도 목표위치에서 가장 가까운 노드까지의 경로 반환
                    return RetracePath(startNode, currentNode);
                }
            }
            
            }
        
            return null;
            
    }
    /// <summary>
    /// maxActionPoints ������ ���� ������ ��� ��带 ����� ��ȯ�ϴ� �޼���
    /// </summary>
    /// <param name="startpos"></param>
    /// <param name="maxActionPoints"></param>
    /// <returns></returns>
    public List<A_Nodes> GetReachableNodes(Vector3 startpos, float maxActionPoints)
    {
        List<A_Nodes> reachableNodes = new List<A_Nodes>();
        A_Nodes startNode = grid.GetNodeFromWorldPoint(startpos);
        List<A_Nodes> openSet = new List<A_Nodes>();
        closedSet.Clear();

        // ���� ��带 openSet�� �߰�
        openSet.Add(startNode);
        startNode.gCost = 0;

        while (openSet.Count > 0)
        {
            A_Nodes currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].gCost < currentNode.gCost ||
                    (openSet[i].gCost == currentNode.gCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            reachableNodes.Add(currentNode);  // ���� ��带 reachableNodes�� �߰�

            // ���� ����� �̿��� Ž��
            foreach (A_Nodes neighbour in grid.GetNeighbours(currentNode, true))
            {
                if (!neighbour.Iswalkable || closedSet.Contains(neighbour))
                    continue;

                float moveCost = GetMoveCost(currentNode, neighbour);
                float newCostToNeighbour = currentNode.gCost + moveCost;

                // ���ο� �̵� ����� maxActionPoints �̳��̸� Ž�� ���
                if (newCostToNeighbour <= maxActionPoints && (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = 0;  // ��ǥ ��尡 �����Ƿ� hCost�� 0���� ����
                    neighbour.parentNode = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);  // openSet�� ���� ��� �߰�
                    }
                }
            }
        }

        return reachableNodes;  // �̵� ������ ��� ��带 ��ȯ
    }
    // ��� ���� �̵� ����� ����ϴ� �Լ�
    private float GetMoveCost(A_Nodes fromNode, A_Nodes toNode)
        {
            // ���� �̵��̸� 1, �밢���̸� 1.4 ���� ������� ����
            return (fromNode.gridX != toNode.gridX && fromNode.gridY != toNode.gridY) ? 1.4f : 1.0f;
        }

        // �� ��� ������ �Ÿ�(hCost)�� ����ϴ� �Լ�
        private float GetDistance(A_Nodes nodeA, A_Nodes nodeB)
        {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (dstX > dstY)
                return 1.4f * dstY + (dstX - dstY);
            return 1.4f * dstX + (dstY - dstX);
        }

        // ��θ� �������ϴ� �Լ�
        private List<A_Nodes> RetracePath(A_Nodes startNode, A_Nodes endNode)
        {
            List<A_Nodes> path = new List<A_Nodes>();
            A_Nodes currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parentNode;
            }

            path.Reverse();
            // ���⿡ ��θ� �׸��ų� ó���ϴ� �߰� ����
            grid.path = path;
        return path;
        }

        // ��带 ���ϴ� Ŀ���� Comparer
        public class NodeComparer : IComparer<A_Nodes>
        {
            public int Compare(A_Nodes nodeA, A_Nodes nodeB)
            {
                // fCost�� ���� ������ ����
                if (nodeA.fCost == nodeB.fCost)
                {
                    // fCost�� ���ٸ� hCost�� ��
                    return nodeA.hCost.CompareTo(nodeB.hCost);
                }
                return nodeA.fCost.CompareTo(nodeB.fCost);
            }
        }
    }
