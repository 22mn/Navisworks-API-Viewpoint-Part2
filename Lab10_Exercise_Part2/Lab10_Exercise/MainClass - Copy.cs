using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;
using Autodesk.Navisworks.Api.Interop.ComApi;
using ComApiBridge = Autodesk.Navisworks.Api.ComApi.ComApiBridge;

using f = System.Windows.Forms;

namespace Lab10_Exercise
{
    [PluginAttribute("Lab10_Exercise", "TwentyTwo", DisplayName = "Lab10_Exec", 
        ToolTip = "A tutorial project by TwentyTwo")]
    public class MainClass : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            // document
            Document doc = Application.ActiveDocument;

            // make a copy of current viewpoint
            Viewpoint vpoint = doc.CurrentViewpoint.CreateCopy();

            #region MoveCamera
            /*
            
            // move to the new position
            Point3D curPos = vpoint.Position;
            Point3D newPos = new Point3D(curPos.X + 10, curPos.Y, curPos.Z);
            vpoint.Position = newPos;

            doc.CurrentViewpoint.CopyFrom(vpoint);
            
            */
            #endregion MoveCamera

            #region RotateCamera
            // collect the rotational axis and angle
            AxisAndAngleResult axisAngleResult = vpoint.Rotation.ToAxisAndAngle();
            // A vector with unit length, represents a direction in 3D space.
            UnitVector3D axis = axisAngleResult.Axis;
            // angle in radians
            double angle = axisAngleResult.Angle;

            // to set values
            // creates a new angle value (90 degree)
            double newAngle = 10 * (3.14 / 180);
            // creates a new axis to rotate (z axis)
            UnitVector3D newAxis = new UnitVector3D(0, 0, 1);
            // creates rotation about given axis by angle in radians (Quaternion)
            Rotation3D newRotation = new Rotation3D(newAxis, newAngle);

            // multiply the current 3D Rotation value with the new value
            vpoint.Rotation = Multiply(vpoint.Rotation, newRotation);

            // update the current viewpoint
            doc.CurrentViewpoint.CopyFrom(vpoint);



            //f.MessageBox.Show(String.Format("Axis : {0} , Angle : {1} ", 
            //axis.ToString(),angle.ToString()));



            #endregion RotateCamera


            return 0;
        }
        public static Rotation3D Multiply(Rotation3D r1, Rotation3D r2)
        {
            Rotation3D res = new Rotation3D(r2.D * r1.A + r2.A * r1.D + r2.B * r1.C - r2.C * r1.B,
                                            r2.D * r1.B + r2.B * r1.D + r2.C * r1.A - r2.A * r1.C,
                                            r2.D * r1.C + r2.C * r1.D + r2.A * r1.B - r2.B * r1.A,
                                            r2.D * r1.D - r2.A * r1.A - r2.B * r1.B - r2.C * r1.C);

            return res;
        }
    }
}
