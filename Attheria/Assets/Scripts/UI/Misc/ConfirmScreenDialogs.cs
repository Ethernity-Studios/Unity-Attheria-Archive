namespace UI
{
    public static class ConfirmScreenDialogs
    {
        /// <summary>
        /// Messages for world deletion
        /// </summary>
        public static readonly string DeleteWorldTitle = "Delete world";
        public static readonly string DeleteWorldDescription = "Are you sure you want to delete this world? \n This will delete all save files associated with it. ";
        public static readonly string DeleteWorldPositiveBtn = "Delete";
        public static readonly string DeleteWorldNegativeBtn = "Cancel";

        /// <summary>
        /// Messages for deleting save files
        /// </summary>
        public static readonly string DeleteSaveTitle = "Delete save";
        public static readonly string DeleteSaveDescription = "Are you sure you want to delete this save?";
        public static readonly string DeleteSaveWorldTitle = "Delete world";
        public static readonly string DeleteSaveWorldDescription = "Are you sure you want to delete this save? \n This will delete associated world with it";
        public static readonly string DeleteSavePositiveBtn = "Delete";
        public static readonly string DeleteSaveNegativeBtn = "Cancel";

        /// <summary>
        /// Messages for exiting world
        /// </summary>
        public static readonly string ExitWorldTitle = "Exit world";
        public static readonly string ExitWorldDescription = "Are you sure you want to exit? All unsaved progress will be lost";
        public static readonly string ExitWorldPositiveBtn = "Exit";
        public static readonly string ExitWorldNegativeBtn = "Cancel";
        
        /// <summary>
        /// Messages for save overriting
        /// </summary>
        public static readonly string OverrideSaveTitle = "Overwrite save";
        public static readonly string OverrideSaveDescription = "Are you sure you want to overwrite this save?";
        public static readonly string OverrideSavePositiveBtn = "Override";
        public static readonly string OverrideSaveNegativeBtn = "Cancel";
        
        public static readonly string LoadSaveTitle = "Load save";
        public static readonly string LoadSaveDescription = "Are you sure you want to load this save? \n All unsaved progress will be lost";
        public static readonly string LoadSavePositiveBtn = "Load";
        public static readonly string LoadSaveNegativeBtn = "Cancel";
    }
}