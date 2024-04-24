using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeSolver
{
    internal class CubeSolverConsts
    {
        //for machine
        internal static int MICROSTEP = 2;
        internal static int STEPS_OF_90DEGREE = 50;
        internal static int STEPS_OF_180DEGREE = 100;
        internal static int STEPS_OF_POS0 = 0;
        internal static int STEPS_OF_POS1 = 54;
        internal static int STEPS_OF_POS2 = 88;
        internal static int STEPS_OF_POS3 = 122;
        internal static int STEPS_OF_POS4 = 156;
        internal static int STEPS_OF_POS5 = 192;
        //internal static int STEPS_OF_FULL_CUBE = 250;redundancy
        internal static int REDUNDANCY_OF_POS0 = 0;
        internal static int REDUNDANCY_OF_POS1 = 3;
        internal static int REDUNDANCY_OF_POS2 = 3;
        internal static int REDUNDANCY_OF_POS3 = 3;
        internal static int REDUNDANCY_OF_POS4 = 3;
        internal static int REDUNDANCY_OF_POS5 = 0;

        //for capture
        internal static int FRAMEWIDTH = 1920;
        internal static int FRAMEHEIGHT = 1080;


        //for recognizer
        internal static int VIEW_TOP = 0;
        internal static int VIEW_LEFT = 515;
        internal static int VIEW_WIDTH = 1080;
        internal static int VIEW_HEIGHT = 1080;

        internal static int ROI_TOP = 170;
        internal static int ROI_LEFT = 590;
        internal static int ROI_WIDTH = 900;
        internal static int ROI_HEIGHT = 900;


        internal static int TOP_OF_FIRST_POINT=82;
        internal static int LEFT_OF_FIRST_POINT = 120;
        internal static int DISTANCE_OF_FACELET = 168;

        internal static int YELLOW_HMIN = 17;
        internal static int YELLOW_HMAX = 53;
        internal static int YELLOW_SMIN = 128;
        internal static int YELLOW_SMAX = 255;
        internal static int YELLOW_VMIN = 46;
        internal static int YELLOW_VMAX = 255;

        internal static int RED_HMIN = 222;
        internal static int RED_HMAX = 255;
        internal static int RED_SMIN = 200;
        internal static int RED_SMAX = 255;
        internal static int RED_VMIN = 46;
        internal static int RED_VMAX = 255;

        internal static int BLUE_HMIN = 148;
        internal static int BLUE_HMAX = 178;
        internal static int BLUE_SMIN = 43;
        internal static int BLUE_SMAX = 255;
        internal static int BLUE_VMIN = 0;
        internal static int BLUE_VMAX = 255;

        internal static int ORANGE_HMIN = 0;
        internal static int ORANGE_HMAX = 16;
        internal static int ORANGE_SMIN = 148;
        internal static int ORANGE_SMAX = 255;
        internal static int ORANGE_VMIN = 46;
        internal static int ORANGE_VMAX = 255;

        internal static int GREEN_HMIN = 78;
        internal static int GREEN_HMAX = 142;
        internal static int GREEN_SMIN = 15;
        internal static int GREEN_SMAX = 255;
        internal static int GREEN_VMIN = 0;
        internal static int GREEN_VMAX = 255;

        internal static int OPEN_ITERATIONS = 3;
        internal static int CLOSE_ITERATIONS = 3;

        internal static int MAX_DISTANCE_RED = 36;
        internal static int MAX_DISTANCE_ORANGE = 36;
    }
}
