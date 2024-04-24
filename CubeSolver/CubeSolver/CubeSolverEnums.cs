using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeSolver
{
    internal enum CommandHeader
    {
        EN,
        X,
        Y,
        Z,
        A,
        B,
        C,
        O,
        S

    }
    public enum InfoDir
    {
        In,
        Out
    }

    public enum CubeFace
    {
        U,
        R,
        F,
        D,
        L,
        B
    }

    public enum Rotater
    {
        X,
        Y,
        Z
    }
    public enum Pole
    {
        A,
        B,
        C
    }
    public enum PoleDirection
    {
        Push,
        Pull
    }

    //从旋转器看向魔方，顺时针为CW、CW2，逆时针为CCW，
    public enum RotateDirection
    {
        CW,
        CCW,
        CW2
    }
    public enum RotateAngle
    {
        D90,
        D180
    }
    public enum CubePos
    {
        Pos0,
        Pos1,
        Pos2,
        Pos3,
        Pos4,
        Pos5
    }
    public enum TwistType
    {
        None,
        U, R, F, D, L, B,
        U1, R1, F1, D1, L1, B1,
        U2, R2, F2, D2, L2, B2,
        Uw, Rw, Fw, Dw, Lw, Bw,
        Uw1, Rw1, Fw1, Dw1, Lw1, Bw1,
        Uw2, Rw2, Fw2, Dw2, Lw2, Bw2,
        u, r, f, d, l, b,
        u1, r1, f1, d1, l1, b1,
        u2, r2, f2, d2, l2, b2
        //x, y, z, x1, y1, z1, x2, y2, z2
    }
}
