using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BisectorMoveUtils
{
    /// <summary>
    /// 获取线的移动后的点
    /// </summary>
    /// <param name="b"></param>
    /// <param name="fromBisector"></param>
    /// <param name="toBisector"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    public static List<Vector2> GetLineMovePoint(Bisector b, Bisector fromBisector, Bisector toBisector, float speed)
    {
        Bisector bb = b.Clone();
        if (float.IsNaN(bb.b))
        {
            bb = new Bisector(new Vector2(b.from.x + speed, b.from.y), new Vector2(b.to.x + speed, b.to.y));
        }
        else
        {
            //线的移动不会改变线的角度，所以只改变线的截距
            bb.b += speed;
        }

        //与唯一点的相交点
        Vector2 fromPoint = fromBisector != null ? fromBisector.Intersection(bb) : new Vector2();
        Vector2 toPoint = toBisector != null ? toBisector.Intersection(bb) : new Vector2();

        return new List<Vector2>() { fromPoint, toPoint };
    }

    public static float GetMoveLineSpeed(Bisector b, float speedX, float speedY)
    {
        float speed;
        if (float.IsNaN(b.k))
        {
            //垂直
            speed = speedX;
        }
        else
        {
            //比较横竖截距
            if (Mathf.Abs(b.b) > Mathf.Abs(b.a))
            {
                speed = speedX;
                if (b.k > 0) speed -= speedY;
                if (b.k < 0) speed += speedY;

                //通过纵截距求横截距
                speed = -b.k * (b.a + speed) - b.b;
            }
            else
            {
                speed = speedY;
                //经过123，134象限的线随x增大而减小
                if (b.k > 0) speed -= speedX;
                //124，234象限反之
                if (b.k < 0) speed += speedX;
            }
        }
        return speed;
    }
}
