namespace TimeSearcher
{
    using System;
    using TimeSearcher.Search;

    public class SharedData
    {
        public static string BaseDir;
        public const int defaultMissingValue = -1;
        public static bool isGraphDisabledHidden = false;
        public static LogTask logTask;
        public static double missingValue;
        public static SearchOptions searchOptions = new SearchOptions();

        public enum TSStatus
        {
            DISABLED,
            ENABLED,
            HIGHLIGHTED,
            ENABLED_AND_SELECTED,
            DISABLED_AND_SELECTED
        }
    }
}

