using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CubeSolver
{
    internal class CubeRecognizer
    {
        //
        Mat clippedImage1;
        Mat clippedImage2;
        Mat clippedImage3;
        Mat clippedImage4;
        Mat clippedImage5;
        Mat clippedImage6;
        Rectangle roi;

        public CubeRecognizer()
        {
            clippedImage1=new Mat();
            clippedImage2=new Mat();
            clippedImage3=new Mat();
            clippedImage4=new Mat();
            clippedImage5=new Mat();
            clippedImage6=new Mat();
            roi=new Rectangle(CubeSolverConsts.ROI_LEFT,CubeSolverConsts.ROI_TOP,CubeSolverConsts.ROI_WIDTH,CubeSolverConsts.ROI_HEIGHT);

        }

        //clipped cube face image
        public Mat Image1
        {
            get
            {
                return clippedImage1;
            }
            set
            {
                clippedImage1 = new Mat();
                clippedImage1 = new Mat(value, roi).Clone();
                value.Save("capture1.jpg");
                clippedImage1.Save("image1.jpg");
            }
        }

        public Mat Image2
        {
            get
            {
                return clippedImage2;
            }
            set
            {
                clippedImage2 = new Mat();
                clippedImage2 = new Mat(value, roi).Clone();
                value.Save("capture2.jpg");
                clippedImage2.Save("image2.jpg");
            }
        }

        public Mat Image3
        {
            get
            {
                return clippedImage3;
            }
            set
            {
                clippedImage3 = new Mat();
                clippedImage3 = new Mat(value, roi).Clone();
                value.Save("capture3.jpg");
                clippedImage3.Save("image3.jpg");
            }
        }

        public Mat Image4
        {
            get
            {
                return clippedImage4;
            }
            set
            {
                clippedImage4 = new Mat();
                clippedImage4 = new Mat(value, roi).Clone();
                value.Save("capture4.jpg");
                clippedImage4.Save("image4.jpg");
            }
        }

        public Mat Image5
        {
            get
            {
                return clippedImage5;
            }
            set
            {
                clippedImage5 = new Mat();
                clippedImage5 = new Mat(value, roi).Clone();
                value.Save("capture5.jpg");
                clippedImage5.Save("image5.jpg");
            }
        }

        public Mat Image6
        {
            get
            {
                return clippedImage6;
            }
            set
            {
                clippedImage6 = new Mat();
                clippedImage6 = new Mat(value, roi).Clone();
                value.Save("capture6.jpg");
                clippedImage6.Save("image6.jpg");
            }
        }



        enum FaceletColor
        {
            None, U, R, F, D, L, B
        }


        static int i = 1;
        public string RegonizeFace(Mat faceImage, bool reverse = false, string savefilename = "", bool filter = false)
        {
            if (faceImage == null) return "";

            string colorstring = "";
            //从左上角色块的中心点开始生成全部25个中心点坐标
            Point[] points = new Point[25];
            Point pointOfTopLeft = new Point(CubeSolverConsts.LEFT_OF_FIRST_POINT , CubeSolverConsts.TOP_OF_FIRST_POINT);
            int distanceOfFacelet = CubeSolverConsts.DISTANCE_OF_FACELET;
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    points[y * 5 + x] = new Point(pointOfTopLeft.X + x * distanceOfFacelet, pointOfTopLeft.Y + y * distanceOfFacelet);
                }
            }
            FaceletColor[] colors = new FaceletColor[25];
            for (int i = 0; i < 25; i++)
            {
                colors[i] = 0;
            }

            //高斯滤波
            if(filter)
            {
                int ksizeX = 13;
                int ksizeY = 13;
                CvInvoke.GaussianBlur(faceImage, faceImage, new Size(ksizeX, ksizeY), 0, 0);
            }

            Mat m = faceImage.Clone();
            int r = 50;//识别结果画圆的半径
            int t = 3;//识别结果画圆的线宽
            Mat hsvImage;
            Mat inRangedImage;
            hsvImage = new Mat();
            CvInvoke.CvtColor(faceImage, hsvImage, Emgu.CV.CvEnum.ColorConversion.Bgr2HsvFull);

            int hMin, hMax, sMin, sMax, vMin, vMax;
            MCvScalar s;
            ScalarArray hsvMin, hsvMax;
            VectorOfVectorOfPoint contours;
            VectorOfRect hierarchy;
            MCvScalar contoursColor;
            int iterations;
            Mat kernel;

#region Yellow
            contoursColor = new MCvScalar(0, 255, 255);
            hMin = CubeSolverConsts.YELLOW_HMIN;
            hMax = CubeSolverConsts.YELLOW_HMAX;
            sMin = CubeSolverConsts.YELLOW_SMIN;
            sMax = CubeSolverConsts.YELLOW_SMAX;
            vMin = CubeSolverConsts.YELLOW_VMIN;
            vMax = CubeSolverConsts.YELLOW_VMAX;
            s = new MCvScalar(hMin, sMin, vMin);
            hsvMin = new ScalarArray(s);
            s = new MCvScalar(hMax, sMax, vMax);
            hsvMax = new ScalarArray(s);

            inRangedImage = new Mat();
            CvInvoke.InRange(hsvImage, hsvMin, hsvMax, inRangedImage);
            inRangedImage.Save($"{savefilename.Substring(0, 1)}-u.{savefilename.Substring(2)}");

            iterations = CubeSolverConsts.OPEN_ITERATIONS;
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(inRangedImage, inRangedImage, Emgu.CV.CvEnum.MorphOp.Open, kernel, new Point(-1, -1), iterations, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar());

            iterations = CubeSolverConsts.CLOSE_ITERATIONS;
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(inRangedImage, inRangedImage, Emgu.CV.CvEnum.MorphOp.Close, kernel, new Point(-1, -1), iterations, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar());

            contours = new VectorOfVectorOfPoint();
            hierarchy = new VectorOfRect();
            CvInvoke.FindContours(inRangedImage, contours, hierarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < 25; i++)
            {
                if (colors[i] == FaceletColor.None)
                {
                    PointF p = points[i];
                    for (int j = 0; j < contours.Size; j++)
                    {
                        if (CvInvoke.PointPolygonTest(contours[j], p, false) > 0)//>0 inside
                        {
                            colors[i] = FaceletColor.U;
                            CvInvoke.Circle(m, new Point((int)p.X, (int)p.Y), r, contoursColor, t);
                            Rectangle rect = CvInvoke.BoundingRectangle(contours[j]);
                            CvInvoke.Rectangle(m, rect, contoursColor, t);
                            break;
                        }
                    }
                }
            }
#endregion

#region Blue
            contoursColor = new MCvScalar(255, 0, 0);
            hMin = CubeSolverConsts.BLUE_HMIN;
            hMax = CubeSolverConsts.BLUE_HMAX;
            sMin = CubeSolverConsts.BLUE_SMIN;
            sMax = CubeSolverConsts.BLUE_SMAX;
            vMin = CubeSolverConsts.BLUE_VMIN;
            vMax = CubeSolverConsts.BLUE_VMAX;
            s = new MCvScalar(hMin, sMin, vMin);
            hsvMin = new ScalarArray(s);
            s = new MCvScalar(hMax, sMax, vMax);
            hsvMax = new ScalarArray(s);

            inRangedImage = new Mat();
            CvInvoke.InRange(hsvImage, hsvMin, hsvMax, inRangedImage);
            inRangedImage.Save($"{savefilename.Substring(0, 1)}-f.{savefilename.Substring(2)}");

            iterations = CubeSolverConsts.OPEN_ITERATIONS;
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(inRangedImage, inRangedImage, Emgu.CV.CvEnum.MorphOp.Open, kernel, new Point(-1, -1), iterations, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar());

            iterations = CubeSolverConsts.CLOSE_ITERATIONS;
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(inRangedImage, inRangedImage, Emgu.CV.CvEnum.MorphOp.Close, kernel, new Point(-1, -1), iterations, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar());

            contours = new VectorOfVectorOfPoint();
            hierarchy = new VectorOfRect();
            CvInvoke.FindContours(inRangedImage, contours, hierarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < 25; i++)
            {
                if (colors[i] == FaceletColor.None)
                {
                    PointF p = points[i];
                    for (int j = 0; j < contours.Size; j++)
                    {
                        if (CvInvoke.PointPolygonTest(contours[j], p, false) > 0)//>0 inside
                        {
                            colors[i] = FaceletColor.F;
                            CvInvoke.Circle(m, new Point((int)p.X, (int)p.Y), r, contoursColor, t);
                            Rectangle rect = CvInvoke.BoundingRectangle(contours[j]);
                            CvInvoke.Rectangle(m, rect, contoursColor, t);
                            break;
                        }
                    }
                }
            }
#endregion

#region Green
            contoursColor = new MCvScalar(0, 255, 0);
            hMin = CubeSolverConsts.GREEN_HMIN;
            hMax = CubeSolverConsts.GREEN_HMAX;
            sMin = CubeSolverConsts.GREEN_SMIN;
            sMax = CubeSolverConsts.GREEN_SMAX;
            vMin = CubeSolverConsts.GREEN_VMIN;
            vMax = CubeSolverConsts.GREEN_VMAX;
            s = new MCvScalar(hMin, sMin, vMin);
            hsvMin = new ScalarArray(s);
            s = new MCvScalar(hMax, sMax, vMax);
            hsvMax = new ScalarArray(s);

            inRangedImage = new Mat();
            CvInvoke.InRange(hsvImage, hsvMin, hsvMax, inRangedImage);
            inRangedImage.Save($"{savefilename.Substring(0, 1)}-b.{savefilename.Substring(2)}");

            iterations = CubeSolverConsts.OPEN_ITERATIONS;
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(inRangedImage, inRangedImage, Emgu.CV.CvEnum.MorphOp.Open, kernel, new Point(-1, -1), iterations, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar());

            iterations = CubeSolverConsts.CLOSE_ITERATIONS;
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(inRangedImage, inRangedImage, Emgu.CV.CvEnum.MorphOp.Close, kernel, new Point(-1, -1), iterations, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar());

            contours = new VectorOfVectorOfPoint();
            hierarchy = new VectorOfRect();
            CvInvoke.FindContours(inRangedImage, contours, hierarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < 25; i++)
            {
                if (colors[i] == FaceletColor.None)
                {
                    PointF p = points[i];
                    for (int j = 0; j < contours.Size; j++)
                    {
                        if (CvInvoke.PointPolygonTest(contours[j], p, false) > 0)//>0 inside
                        {
                            colors[i] = FaceletColor.B;
                            CvInvoke.Circle(m, new Point((int)p.X, (int)p.Y), r, contoursColor, t);
                            Rectangle rect = CvInvoke.BoundingRectangle(contours[j]);
                            CvInvoke.Rectangle(m, rect, contoursColor, t);
                            break;
                        }
                    }
                }
            }
#endregion

#region Red
            contoursColor = new MCvScalar(0, 0, 255);
            hMin = CubeSolverConsts.RED_HMIN;
            hMax = CubeSolverConsts.RED_HMAX;
            sMin = CubeSolverConsts.RED_SMIN;
            sMax = CubeSolverConsts.RED_SMAX;
            vMin = CubeSolverConsts.RED_VMIN;
            vMax = CubeSolverConsts.RED_VMAX;
            s = new MCvScalar(hMin, sMin, vMin);
            hsvMin = new ScalarArray(s);
            s = new MCvScalar(hMax, sMax, vMax);
            hsvMax = new ScalarArray(s);

            inRangedImage = new Mat();
            CvInvoke.InRange(hsvImage, hsvMin, hsvMax, inRangedImage);
            inRangedImage.Save($"{savefilename.Substring(0, 1)}-r.{savefilename.Substring(2)}");

            iterations = CubeSolverConsts.OPEN_ITERATIONS;
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(inRangedImage, inRangedImage, Emgu.CV.CvEnum.MorphOp.Open, kernel, new Point(-1, -1), iterations, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar());

            iterations = CubeSolverConsts.CLOSE_ITERATIONS;
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(inRangedImage, inRangedImage, Emgu.CV.CvEnum.MorphOp.Close, kernel, new Point(-1, -1), iterations, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar());

            contours = new VectorOfVectorOfPoint();
            hierarchy = new VectorOfRect();
            CvInvoke.FindContours(inRangedImage, contours, hierarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < 25; i++)
            {
                if (colors[i] == FaceletColor.None)
                {
                    PointF p = points[i];
                    for (int j = 0; j < contours.Size; j++)
                    {
                        if (CvInvoke.PointPolygonTest(contours[j], p, false) > 0)//>0 inside
                        {
                            colors[i] = FaceletColor.R;
                            CvInvoke.Circle(m, new Point((int)p.X, (int)p.Y), r, contoursColor, t);
                            Rectangle rect = CvInvoke.BoundingRectangle(contours[j]);
                            CvInvoke.Rectangle(m, rect, contoursColor, t);
                            break;
                        }
                    }
                    if (colors[i] == FaceletColor.None)
                    {
                        for (int j = 0; j < contours.Size; j++)
                        {
                            double dis = CvInvoke.PointPolygonTest(contours[j], p, true);
                            if (Math.Abs(dis) < CubeSolverConsts.MAX_DISTANCE_RED)//红色轮廓不一定能包住点，附加使用距离绝对值
                            {
                                colors[i] = FaceletColor.R;
                                CvInvoke.Circle(m, new Point((int)p.X, (int)p.Y), r, contoursColor, t);
                                Rectangle rect = CvInvoke.BoundingRectangle(contours[j]);
                                CvInvoke.Rectangle(m, rect, contoursColor, t);
                                break;
                            }
                        }
                    }
                }
            }
#endregion

#region Orange
            contoursColor = new MCvScalar(0, 64, 128);
            hMin = CubeSolverConsts.ORANGE_HMIN;
            hMax = CubeSolverConsts.ORANGE_HMAX;
            sMin = CubeSolverConsts.ORANGE_SMIN;
            sMax = CubeSolverConsts.ORANGE_SMAX;
            vMin = CubeSolverConsts.ORANGE_VMIN;
            vMax = CubeSolverConsts.ORANGE_VMAX;
            s = new MCvScalar(hMin, sMin, vMin);
            hsvMin = new ScalarArray(s);
            s = new MCvScalar(hMax, sMax, vMax);
            hsvMax = new ScalarArray(s);

            inRangedImage = new Mat();
            CvInvoke.InRange(hsvImage, hsvMin, hsvMax, inRangedImage);
            inRangedImage.Save($"{savefilename.Substring(0,1)}-l.{savefilename.Substring(2)}");

            iterations = CubeSolverConsts.OPEN_ITERATIONS;
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(inRangedImage, inRangedImage, Emgu.CV.CvEnum.MorphOp.Open, kernel, new Point(-1, -1), iterations, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar());

            iterations = CubeSolverConsts.CLOSE_ITERATIONS;
            kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
            CvInvoke.MorphologyEx(inRangedImage, inRangedImage, Emgu.CV.CvEnum.MorphOp.Close, kernel, new Point(-1, -1), iterations, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar());

            contours = new VectorOfVectorOfPoint();
            hierarchy = new VectorOfRect();
            CvInvoke.FindContours(inRangedImage, contours, hierarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < 25; i++)
            {
                if (colors[i] == FaceletColor.None)
                {
                    PointF p = points[i];
                    for (int j = 0; j < contours.Size; j++)
                    {
                        if (CvInvoke.PointPolygonTest(contours[j], p, false) > 0)//>0 inside
                        {
                            colors[i] = FaceletColor.L;
                            CvInvoke.Circle(m, new Point((int)p.X, (int)p.Y), r, contoursColor, t);
                            Rectangle rect = CvInvoke.BoundingRectangle(contours[j]);
                            CvInvoke.Rectangle(m, rect, contoursColor, t);
                            break;
                        }
                    }
                    if (colors[i] == FaceletColor.None)
                    {
                        for (int j = 0; j < contours.Size; j++)
                        {
                            double dis = CvInvoke.PointPolygonTest(contours[j], p, true);
                            if (Math.Abs(dis) < CubeSolverConsts.MAX_DISTANCE_ORANGE)//橙色轮廓不一定能包住点，附加使用距离绝对值
                            {
                                colors[i] = FaceletColor.L;
                                CvInvoke.Circle(m, new Point((int)p.X, (int)p.Y), r, contoursColor, t);
                                Rectangle rect = CvInvoke.BoundingRectangle(contours[j]);
                                CvInvoke.Rectangle(m, rect, contoursColor, t);
                                break;
                            }
                        }
                    }
                }
            }
#endregion

#region White
            contoursColor = new MCvScalar(255, 255, 255);
            for (int i = 0; i < 25; i++)
            {
                if (colors[i] == FaceletColor.None)
                {
                    colors[i] = FaceletColor.D;
                    PointF p = points[i];
                    CvInvoke.Circle(m, new Point((int)p.X, (int)p.Y), r, contoursColor, t);
                }
            }
#endregion

            if (reverse)
            {
                colorstring = string.Join("", colors.Reverse<FaceletColor>().ToArray<FaceletColor>());
            }
            else
            {
                colorstring=string.Join("", colors);
            }

            if (savefilename != "")
            {
                m.Save(savefilename);
            }

            return colorstring;
        }

    }
}
