﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KristofferStrube.Blazor.SVGEditor
{
    public class QuadraticBézierCurveInstruction : BaseControlPointPathInstruction
    {
        public QuadraticBézierCurveInstruction(double x1, double y1, double x, double y, IPathInstruction PreviousInstruction, bool Relative) : base(PreviousInstruction)
        {
            this.Relative = Relative;
            this.PreviousInstruction = PreviousInstruction;
            if (Relative)
            {
                this.ControlPoints.Add((StartPosition.x + x1, StartPosition.y + y1));
                this.x = StartPosition.x + x;
                this.y = StartPosition.y + y;
            }
            else
            {
                this.ControlPoints.Add((x1, y1));
                this.x = x;
                this.y = y;
            }
        }

        private double x { get; set; }

        private double y { get; set; }

        public override (double x, double y) EndPosition
        {
            get { return (x, y); }
            set { x = value.x; y = value.y; }
        }

        public override string AbsoluteInstruction => "Q";

        public override string RelativeInstruction => "q";

        public override string ToString() => (ExplicitSymbol ? $"{(Relative ? RelativeInstruction : AbsoluteInstruction)} " : "") +
            (Relative ?
            $"{(ControlPoints[0].x - StartPosition.x).AsString()} {(ControlPoints[0].y - StartPosition.y).AsString()} {(x - StartPosition.x).AsString()} {(y - StartPosition.y).AsString()}" :
            $"{ControlPoints[0].x.AsString()} {ControlPoints[0].y.AsString()} {x.AsString()} {y.AsString()}");
    }
}
