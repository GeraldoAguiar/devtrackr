using DEVTRACKR.API.Entities;

namespace DEVTRACKR.API.Persistence;

public class DevTrackRContext
{
    public DevTrackRContext()
    {
        Packages = new List<Package>();
    }
    public List<Package> Packages { get; set; }
}