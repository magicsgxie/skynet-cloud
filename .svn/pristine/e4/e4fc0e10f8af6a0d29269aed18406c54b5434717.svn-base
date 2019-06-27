//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace UWay.Skynet.Cloud
//{
    

//    class Test
//    {
//        public static void Main(string[] args)
//        {

//        }


//        /// <summary>
//        /// 计算与圆相交坐标
//        /// </summary>
//        /// <param name="Aux1">参考点1</param>
//        /// <param name="Aux2">参考点1</param>
//        /// <param name="r">半径</param>
//        /// <param name="center">圆心</param>
//        /// <returns></returns>
//        static PointV[] GetCirPoint(PointV Aux1, PointV Aux2, double r, PointV center )
//        {

//            PointV[] points = new PointV[2];

//            //直线斜率不存在，线段垂直于X轴
//            if ((Aux1.x == Aux2.x) && (Aux1.y != Aux2.y))
//            {
//                if (Math.Abs(center.x - Aux1.x) < r)
//                {
//                    double y = Math.Sqrt(r * r - ((Aux1.x - center.x) * (Aux1.x - center.x)));
//                    points[0].x = Aux1.x;
//                    points[0].y = center.y + y;
//                    points[1].x = Aux1.x;
//                    points[1].y = center.y - y;
//                }
//            }
//            //两点重合
//            else if ((Aux1.y == Aux2.y) && (Aux1.x == Aux2.x))
//            {
//                points[0].x = Aux1.x;
//                points[0].y = Aux1.y;
//                points[1].x = Aux2.x;
//                points[1].y = Aux2.y;
//            }
//            //直线斜率为0，平行于X轴
//            else if ((Aux1.y == Aux2.y) && (Aux1.x != Aux2.x))
//            {
//                double area = fabs((center.y) - (Aux1.y));
//                if (area <= r)
//                {
//                    double x = sqrt(r * r - (Aux1.y - center.y) * (Aux1.y - center.y));
//                    points[0].x = center.x + x;
//                    points[0].y = Aux1.y;
//                    points[1].x = center.x - x;
//                    points[1].y = Aux1.y;
//                }
//            }
//            else
//            {
//                double k = (Aux2.y - Aux1.y) / (Aux2.x - Aux1.x);
//                double b = Aux2.y - k * (Aux2.x);
//                double del = 4 * pow((k * b - center.x - k * (center.y)), 2) - 4 * (1 + k * k) * (pow((center.x), 2) + pow((b - center.y), 2) - r * r);
//                if (del > 0)
//                {
//                    double tmp = 2 * (k * b - center.x - k * center.y);
//                    points[0].x = (-tmp + sqrt(del)) / (2 * (1 + k * k));
//                    points[0].y = k * (points[0].x) + b;
//                    points[1].x = (-tmp - sqrt(del)) / (2 * (1 + k * k));
//                    points[1].y = k * (points[1].x) + b;
//                }
//            }
//            return points;
//        }

        
//    }

//    public class PointV
//    {
//        public double x { get; set; }
//        public double y { get; set; }
//    }
//}
