namespace InitializableLogic.Misc
{
    public static class InitializableVariables
    {
#if UNITY_EDITOR
        public const bool CLEAR_SUBSCRIBERS_LIST = false;
#else
        public const bool CLEAR_SUBSCRIBERS_LIST = true;
#endif
    }
}

