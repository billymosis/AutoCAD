using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace Study2
{
    public class Class3

    {
        [CommandMethod("QE")]
        public void QE()
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;


            PromptResult result = ed.GetKeywords(new PromptKeywordOptions("choose your character [Select / All] :", "Select All"));
            switch (result.StringResult)
            {
                case "All":
                    using (Transaction tr = doc.TransactionManager.StartTransaction())
                    {

                        BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForWrite) as BlockTable;
                        BlockTableRecord btr = tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

                        foreach (var item in btr)
                        {
                            try
                            {
                                var c = item.GetObject(OpenMode.ForWrite) as Circle;
                                c.Diameter = 10;
                            }
                            catch (System.Exception ex)
                            {
                                ed.WriteMessage(ex.Message);
                            }

                        }
                        tr.Commit();

                    }

                    ed.WriteMessage("\nEnd of Transaction");
                    break;

                case "Select":
                    using (Transaction tr = doc.TransactionManager.StartTransaction())
                    {

                        PromptSelectionResult selectionResult = ed.GetSelection();
                        PromptDoubleResult d = ed.GetDouble("\nDiameter: ");
                        foreach (var item in selectionResult.Value.GetObjectIds())
                        {
                            try
                            {
                                var c = item.GetObject(OpenMode.ForWrite) as Circle;
                                c.Diameter = d.Value;
                            }
                            catch (System.Exception ex)
                            {
                                ed.WriteMessage(ex.Message);
                            }

                        }

                        tr.Commit();
                    }
                    break;
            }


        }
    }
}
