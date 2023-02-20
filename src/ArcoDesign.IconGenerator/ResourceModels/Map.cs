namespace ArcoDesign.IconGenerator.ResourceModels;
public class Map {
    public MapItem ZhCN { get; set; }
    public MapItem EnUS { get; set; }
}
public class MapItem {
    public string direction { get; set; }
    public string tips { get; set; }
    public string interactivebutton { get; set; }
    public string edit { get; set; }
    public string media { get; set; }
    public string logo { get; set; }
    public string general { get; set; }
}
