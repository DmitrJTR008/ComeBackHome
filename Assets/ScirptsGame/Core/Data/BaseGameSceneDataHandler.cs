using System;
    public class BaseGameSceneDataHandler
    {
        protected Action<IDataSaveable> OnDataLoad;
        public Action<IDataSaveable> OnDataLoadComplete;
        public BaseGameSceneDataHandler GetClass()
        {
            return this;
        }
    }