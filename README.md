# MntopoSpotElevation
ArcGIS Pro Map Tool Add-In for querying a point elevation from [MnTOPO](http://arcgis.dnr.state.mn.us/maps/mntopo/).

The add in allows the user to query an elevation for any point located within Minnesota. The tool sends the longitude and latitude of the selected location to the MnTOPO API endpoint [GetElevationAtLonLat](https://arcgis.dnr.state.mn.us/mndnr/rest/services/elevation/DEM1m/MapServer/exts/ElevationsSOE_NET/ElevationLayers/0/GetElevationAtLonLat). The server returns an elevation from the 1-meter DEM data set which is converted to feet and presented to the user as a message box popup.
