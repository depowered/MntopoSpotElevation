using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Mapping;
using System;
using System.Threading.Tasks;

namespace MntopoSpotElevation
{
    internal class MapTool1 : MapTool
    {
        public MapTool1()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Point;
            SketchOutputMode = SketchOutputMode.Map;
        }

        protected override Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            var coord = GeometryEngine.Instance.Project(geometry, SpatialReferences.WGS84) as MapPoint;
            if (coord != null)
            {
                double lon = coord.X;
                double lat = coord.Y;

                // query MnTOPO
                double elevationFeet = MntopoResponse.GetElevationAtLonLatFeet(lon, lat);
                if (elevationFeet < 0) // GetElevationAtLonLatFeet will return -1 if the server retruns an error.
                {
                    string msg = "The location selected is outside of the data extents \nPlease try again.";
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(msg);
                    return Task.FromResult(false);
                }
                else  // server returned a result
                {
                    string msg = $"Elevation: {elevationFeet} feet";
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(msg);
                    return Task.FromResult(true);
                }
            }
            else
            {
                String msg = "Selected coordinate is empty.";
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show(msg);
                return Task.FromResult(false);
            }
        }
    }
}
