using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.IFC;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using System.Collections.Generic;


using Revit.IFC.Export.Exporter;
using Revit.IFC.Export.Utility;


namespace RoBIM
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class StructuralFraming : IExternalCommand
    {   

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            ICollection<Reference> reference_collector;
            UIDocument uidoc;
            uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Application app = doc.Application;
            UIApplication uiapp = new UIApplication(app);

            ExporterIFC exporterIFC;
            
            //ExporterIFC.ExportIFC(doc, exporterIFC, doc.ActiveView);
            //IFCExportInfoPair exportType=new IFCExportInfoPair();
            //ProductWrapper productWrapper = new ProductWrapper();

            Reference reference = uidoc.Selection.PickObject(ObjectType.Element);
            Autodesk.Revit.DB.Element element = doc.GetElement(reference);
            Options geomOptions = new Options();
            geomOptions.ComputeReferences = true;
            GeometryElement geometryElement = element.get_Geometry(geomOptions);

            
            //Define Document
            BeamExporter beamExporter=new BeamExporter();

            //beamExporter.ExportBeamAsStandardElement(exporterIFC, element, exportType, 
            //geometryElement, productWrapper, out bool dontExport);
            Transaction trans= new Transaction(doc);
            trans.Start("");
            trans.Commit();
            return Result.Succeeded;
        }

    }
}
