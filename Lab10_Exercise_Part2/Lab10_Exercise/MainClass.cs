
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Plugins;

namespace Lab10_Exercise
{
    [PluginAttribute("Lab10_Exec_Part2", "TwentyTwo", DisplayName = "Lab10_Exec_Part2", 
        ToolTip = "A tutorial project by TwentyTwo")]
    public class MainClass : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            // document
            Document doc = Application.ActiveDocument;
            // make a copy of current viewpoint
            Viewpoint vpoint = doc.CurrentViewpoint.CreateCopy();
            #region get-cam
            /*
            // get cam value
            string camVal = vpoint.GetCamera();
            //write string to file
            System.IO.File.WriteAllText(@"D:\Directory\FileName.txt", camVal);
            */

            #endregion get-cam
            #region set-cam
            /*
            // set cam value
            // file path
            string path = @"D:\Directory\FileName.json";
            // read file
            string file = System.IO.File.ReadAllText(path);
            // set cam value
            vpoint.SetCamera(file);
            // update viewpoint
            doc.CurrentViewpoint.CopyFrom(vpoint);
            */
            #endregion set-cam

            // currentselection check
            if (!doc.CurrentSelection.SelectedItems.IsEmpty)
            {
                // current selected items
                ModelItemCollection modelItems = doc.CurrentSelection.SelectedItems;
                // items' bbox
                BoundingBox3D bbox = modelItems.BoundingBox(true);
                // zoom viewpoint
                vpoint.ZoomBox(bbox);
                // update viewpoint
                doc.CurrentViewpoint.CopyFrom(vpoint);
            }
            return 0;
        }
    }
}
