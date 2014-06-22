namespace TimeSearcher.Filters
{
    using System;
    using System.Collections;
    using TimeSearcher;

    public class TFNEntity : DisablingEntity
    {
        private ArrayList _alFilteredNames;
        private DisablingManager _disablingManager;
        private static TFNEntity _prevTFNEntity;

        public TFNEntity(ArrayList filteredNames, DisablingManager disablingManager)
        {
            this._alFilteredNames = filteredNames;
            this._disablingManager = disablingManager;
            replacePrevWith(this);
        }

        public void Dematerialize()
        {
            this._disablingManager.RemoveEntity(this);
        }

        public bool Filters(DataItem di)
        {
            return this._alFilteredNames.Contains(di.Name);
        }

        private static void replacePrevWith(TFNEntity entity)
        {
            if (_prevTFNEntity != null)
            {
                _prevTFNEntity.Dematerialize();
            }
            _prevTFNEntity = entity;
        }
    }
}

