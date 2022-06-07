using System.Runtime.InteropServices;

public static class MyPlot
{
    public struct KeywordValue
    {
        public KeywordValue(string keyword, string value)
        {
            this.keyword = keyword;
            this.value = value;
        }

        private string keyword;
        private string value;
    }

    [DllImport("MyPlot")]
    public static extern bool Plot(float[] x, float[] y, int xySize, KeywordValue[] kw, int kwSize);
    
    [DllImport("MyPlot")]
    public static extern bool Quiver(float[] x, float[] y, float[] u, float[] v, int xyuvSize, KeywordValue[] kw, int kwSize);

    [DllImport("MyPlot")]
    public static extern void SubPlot(int nRows, int nCols, int index);

    [DllImport("MyPlot")]
    public static extern bool Scatter(float[] x, float[] y, int xySize, KeywordValue[] kw, int kwSize, float s = 1.0f);

    [DllImport("MyPlot")]
    public static extern void Autoscale(bool enable = true, string axis = "both", bool tight = false);

    [DllImport("MyPlot")]
    public static extern void LabelLines();

    [DllImport("MyPlot")]
    public static extern void Clf();

    [DllImport("MyPlot")]
    public static extern void Shutdown();
}