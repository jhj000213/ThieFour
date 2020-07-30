using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

    public float _viewRadius;
    [Range(0,360)]
    public float _viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    
    public List<Player_InHouse> _VisibleTargets = new List<Player_InHouse>();

    public MeshRenderer ViewMeshRen = null;
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    public float meshResolution;
    public int edgeResolveIterations;

    public float edgeDstThreshold;

    public bool _CCTV;

    public bool _Find;

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        ViewMeshRen.sortingOrder = 4000;
        ViewMeshRen.sharedMaterial.renderQueue = 4000;
        
        
    }

    void LateUpdate()
    {
        DrawFieldView();
        if(_CCTV)
        {
            transform.localEulerAngles = new Vector3(0, -(transform.parent.transform.localEulerAngles.z-90), 0);
        }
    }

    void Update()
    {
        _VisibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, _viewRadius, targetMask);
        
        for(int i=0;i<targetsInViewRadius.Length;i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            float angle = -transform.localEulerAngles.y+90;

            Vector3 betweenpos = target.position-transform.position;
            float deg = Mathf.Atan2(betweenpos.y, betweenpos.x) * Mathf.Rad2Deg;

            if (angle <-180)
                angle = angle+360;


            Vector3 direction = new Vector3(Mathf.Cos(deg * Mathf.Deg2Rad) * _viewRadius, Mathf.Sin(deg * Mathf.Deg2Rad) * _viewRadius);
            if (Mathf.Abs(angle - deg) < _viewAngle / 2 || 360 - _viewAngle / 2 < Mathf.Abs(angle - deg))
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics2D.Raycast(transform.position, direction, dstToTarget, obstacleMask))
                {
                    if (target.tag == "player" && !target.GetComponent<Player_InHouse>()._Escaper_Camouflage && 
                        !target.GetComponent<Player_InHouse>()._Dead&&!target.GetComponent<Player_InHouse>()._EscapingClear)
                    {
                        _VisibleTargets.Add(target.GetComponent<Player_InHouse>());
                        if (!_Find)
                            _Find = true;
                    }
                }
            }
        }
    }

    void DrawFieldView()
    {
        int stepCount = Mathf.RoundToInt(_viewAngle * meshResolution);
        float stepAngleSize = _viewAngle / stepCount;

        List<Vector3> viewPoints = new List<Vector3>();

        ViewCastInfo oldViewCast = new ViewCastInfo();

        for(int i=0;i<=stepCount;i++)
        {
            float angle = transform.localEulerAngles.y - _viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if(i>0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast._distance - newViewCast._distance) > edgeDstThreshold;

                if(oldViewCast._hit != newViewCast._hit || (oldViewCast._hit && newViewCast._hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if(edge._pointA!=Vector3.zero)
                    {
                        viewPoints.Add(edge._pointA);
                    }
                    if (edge._pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge._pointB);
                    } 
                }
            }

            viewPoints.Add(newViewCast._point);
            oldViewCast = newViewCast;
        }

        int vertextCount = viewPoints.Count + 1;
        Vector3[] virtices = new Vector3[vertextCount];
        int[] triangles = new int[(vertextCount - 2) * 3];

        virtices[0] = Vector3.zero;

        for(int i=0;i<vertextCount-1;i++)
        {
            virtices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if(i<vertextCount-2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

            viewMesh.Clear();
            viewMesh.vertices = virtices;
            viewMesh.triangles = triangles;
            viewMesh.RecalculateNormals();
       

        //ViewMeshRen.sortingOrder = 4000;
        //ViewMeshRen.sharedMaterial.renderQueue = 4000;
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast._angle;
        float maxAngle = maxViewCast._angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for(int i=0;i<edgeResolveIterations;i++)
        {
            float angle = (minAngle+maxAngle)/2;
            ViewCastInfo newViewCast = ViewCast(angle);
            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast._distance - newViewCast._distance) > edgeDstThreshold;


            if(newViewCast._hit == minViewCast._hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast._point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast._point;
            }
        }
        return new EdgeInfo(minPoint,maxPoint);
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position,dir,_viewRadius,obstacleMask);
        if(hit.collider!=null)
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position+dir*_viewRadius, _viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees,bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);

    }

    public struct ViewCastInfo
    {
        public bool _hit;
        public Vector3 _point;
        public float _distance;
        public float _angle;

        public ViewCastInfo(bool hit,Vector3 point,float dst,float angle)
        {
            _hit = hit;
            _point = point;
            _distance = dst;
            _angle = angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 _pointA;
        public Vector3 _pointB;

        public EdgeInfo(Vector3 pointA, Vector3 pointB)
        {
            _pointA=pointA;
            _pointB=pointB;
        }
    }
}
