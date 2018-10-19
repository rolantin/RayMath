using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PlaneMath : MonoBehaviour {

    public Transform mSun;  //规定一个太阳

    public LineRenderer inline1;
    public LineRenderer inline2;
    public LineRenderer inline3;
    public LineRenderer outline1;
    public LineRenderer outline2;
    public LineRenderer outline3;

    private Mesh mMesh;

    private Vector3 p1;
    private Vector3 p2;
    private Vector3 p3;

    private Vector3 v1;
    private Vector3 v2;

    private Vector3 n;

    private Vector3 L1;
    private Vector3 L2;
    private Vector3 L3;

    private Vector3 outRay1;
    private Vector3 outRay2;
    private Vector3 outRay3;


    void Update()
    {
        mMesh = GetComponent<MeshFilter>().mesh;
        //选取平面网格的任意三个点
        p1 = mMesh.vertices[20];
        p2 = mMesh.vertices[36];
        p3 = mMesh.vertices[45];
        //计算出两条任意向量
        v1 = p2 - p1;
        v2 = p3 - p1;
        //计算平面单位法向量n
        Vector3 N = Vector3.Cross(v1, v2);
        n = Vector3.Normalize(N);
        //计算出三条入射光线向量
        L1 = p1 - mSun.position;
        Debug.Log(L1);
        L2 = p2 - mSun.position;
        L3 = p3 - mSun.position;
        //通过博客公式计算出三条反射光线向量
     //   Vector3 c = Vector3.Dot(-L1, n) * n;
     //  outRay1 =  inRay - 2 *c ;
      // outRay1 = L1 - 2 *( L1 + Vector3.Dot(-L1,n)*n );
        Vector3 c = Vector3.Dot(L1,n)*n;
        Vector3 a= L1-c;
        Vector3 s = a *2;
        outRay1 = -L1 + s;


      // outRay1 = -L1 + (2*(L1 - Vector3.Dot(-L1,n)*n ));
        //outRay1 =L2 - Vector3.Dot (L1, n) * n;
        outRay2 = L2 - 2 * Vector3.Dot(L2, n) * n;
        outRay3 = L3 - 2 * Vector3.Dot(L3, n) * n;
        //计算完了，这里我们实际展示下光线的路径
        AssitDraw();
    }

    //辅助展示函数
    void AssitDraw()
    {
        //画出入射光
        inline1.positionCount = 2;
        inline1.SetPosition(0, mSun.position);
        inline1.SetPosition(1, p1);

        inline2.positionCount = 2;
        inline2.SetPosition(0, mSun.position);
        inline2.SetPosition(1, p2);

        inline3.positionCount = 2;
        inline3.SetPosition(0, mSun.position);
        inline3.SetPosition(1, p3);

        //画出反射光线
        Vector3 op1 = calculateLinePoint(p1, Vector3.Normalize(outRay1), 10);
        outline1.positionCount = 2;
        outline1.SetPosition(0, p1);
        outline1.SetPosition(1, op1);

        Vector3 op2 = calculateLinePoint(p2, Vector3.Normalize(outRay2), 10);
        outline2.positionCount = 2;
        outline2.SetPosition(0, p2);
        outline2.SetPosition(1, op2);

        Vector3 op3 = calculateLinePoint(p3, Vector3.Normalize(outRay3), 10);
        outline3.positionCount = 2;
        outline3.SetPosition(0, p3);
        outline3.SetPosition(1, op3);
    }

    /// <summary>
    /// 计算朝向单位向量direction直线上从起点bpoint模长length的终点
    /// </summary>
    /// <param name="bpoint"></param>
    /// <param name="direction"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    Vector3 calculateLinePoint(Vector3 bpoint, Vector3 direction, float length)
    {
        float x, y, z;
        x = bpoint.x + direction.x * length;
        y = bpoint.y + direction.y * length;
        z = bpoint.z + direction.z * length;
        return new Vector3(x, y, z);
    }

	void Start () {
		
	}
	
	
}
