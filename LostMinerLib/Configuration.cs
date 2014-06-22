namespace TimeSearcher
{
    using System;
    using System.Drawing;

    public class Configuration
    {
        public static Brush appBackgroundBrush = Brushes.White;
        public static Color appBackgroundColor = Color.White;
        public static Pen borderSearchBoxPen = new Pen(Brushes.Orange, 3f);
        public static Pen boxHandleBackgroundPen = new Pen(Brushes.White, 1f);
        public const int boxHandleHalfSide = 3;
        public static Pen defaultDataVariablePen = new Pen(Brushes.Green, 1f);
        public static Pen enabledTimeBoxPen = graphEnabledPen;
        public static Brush eraserBrush = Brushes.White;
        public static Pen graphDisabledPen = new Pen(Color.FromArgb(0xe8, 0xe8, 0xe8), 2f);
        public static Pen graphDisabledPen_Smooth = new Pen(Color.FromArgb(0xe8, 0xe8, 0xe8), 1f);
        public static Pen graphEnabledPen = new Pen(Brushes.SlateGray, 2f);
        public static Pen graphEnabledPen_Smooth = new Pen(Brushes.Red, 1f);
        public static Pen graphEraseDisabledPen = new Pen(eraserBrush, 2f);
        public static Pen graphEraseDisabledPen_Smooth = new Pen(eraserBrush, 1f);
        public static Pen graphEraseEnabledPen = new Pen(eraserBrush, 1f);
        public static Pen graphEraseHighlightedPen = new Pen(Brushes.White, 2f);
        public static Pen graphEraseMatchedPen = new Pen(eraserBrush, 3.5f);
        public static Pen graphEraseSelectedPen = new Pen(eraserBrush, 2f);
        public static Pen graphHighlightedPen = new Pen(Brushes.Orange, 2f);
        public static Pen graphHighlightedPen_Smooth = new Pen(Brushes.Orange, 2f);
        public static Pen graphMatchedPen = new Pen(Brushes.Red, 2f);
        public static Pen graphMatchedPen_Smooth = new Pen(Brushes.Red, 3.5f);
        public static Pen graphSelectedPen = new Pen(Brushes.Blue, 2f);
        public static Pen graphSelectedPen_Smooth = new Pen(Brushes.Blue, 2f);
        public const int graphSelectionTolerance = 10;
        public static Pen gridPen = new Pen(Brushes.LightGray, 1f);
        public static Pen highlightedTimeBoxPen = graphHighlightedPen;
        public static Color incompleteTimeBoxColor = Color.FromArgb(0xe8, Color.LightBlue);
        public static bool isGridVisible;
        public static int maxWidthSearchButton = 20;
        public static Pen missingValuePen = new Pen(Brushes.Red, 1f);
        public static Pen myArrowBorderPen = new Pen(Brushes.Black, 2f);
        public static Pen overviewBoxPen = new Pen(Brushes.Orange, 4f);
        public static Pen riverGraphAfterForecastPen = new Pen(Brushes.Red, 3f);
        public static Pen riverGraphBeforeForecastPen = riverGraphMedianPen;
        public static Pen riverGraphDuringForecastPen = new Pen(Brushes.Brown, 3f);
        public static Brush[] riverGraphGapBrushes = new Brush[] { Brushes.LightGray, Brushes.DarkGray, Brushes.DarkGray, Brushes.LightGray };
        public static Pen riverGraphIncompletePen = new Pen(Brushes.Gold, 2.5f);
        public static int riverGraphMedianIndex;
        public static Pen riverGraphMedianPen = new Pen(Brushes.Black, 3f);
        public static Pen riverGraphRegularPen = Pens.SlateGray;
        //分析框内部颜色
       
        public static Pen searchBoxPen = new Pen(Brushes.DarkSeaGreen, 1f);
        public static float searchBoxTolerance = 3f;
        public static Brush searchButtonArrowBrush = Brushes.Green;
        public static int searchButtonDefaultWidth = 10;
        public static Pen searchPatternTriangleBorderPen = new Pen(Brushes.DarkGreen, 1f);
        public static Color searchPatternTriangleDarkColor = Color.DarkGreen;
        public static Color searchPatternTriangleLiteColor = Color.LightGreen;
        public static Pen searchResultTriangleBorderPen = new Pen(Brushes.DarkRed, 1f);
        public static Color searchResultTriangleDarkColor = Color.DarkRed;
        public static Color searchResultTriangleLiteColor = Color.Red;
        public static Pen selectedBborderSearchBoxPen = new Pen(Brushes.Yellow, 1f);
        public static Pen selectedBorderBoxHandlePen = graphHighlightedPen;
        //删除框内部颜色
        public static Pen timeBoxPen = new Pen(Brushes.Red, 1f);
        public static Pen timeLinePen = new Pen(Brushes.LightBlue, 2f);
        public static Pen timeLinePenWarring = new Pen(Brushes.Red, 2f);
        public static Pen timePointBarPen = new Pen(Brushes.LightGray, 1f);
        public static Brush timePointBrush = Brushes.Black;
        public static Brush toleranceIndicatorBackgroundSelected = Brushes.Orange;
        public static Pen toleranceIndicatorBorderSelected = new Pen(Brushes.Yellow, 1f);
        public static int triangleDim = 10;
        public static Pen unselectedBorderBoxHandlePen = new Pen(Brushes.Black, 1f);
        public static Pen unSelectedBorderSearchBoxPen = new Pen(Brushes.Orange, 1f);
        private const float w = 2f;
        public static Pen wran_pen = new Pen(Brushes.LightGray, 0.8f);
        public static Pen yValueBarPen = new Pen(Brushes.Red, 1f);

        private Configuration()
        {
        }
    }
}

