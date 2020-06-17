namespace SolarAnalysisLogic.Models
{
    public interface IBuildingModel
    {
        int height { get; set; }
        string label { get; set; }
        int riseLocation { get; set; }
        int setLocation { get; set; }
        int width { get; set; }
        bool seeSunSet { get; set; }
        bool seeSunRise { get; set; }
        double sunTime { get; set; }
    }
}