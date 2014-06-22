namespace TimeSearcher
{
    using System;
    using System.Collections;
    using TimeSearcher.Filters;

    public class DisablingManager
    {
        private readonly DataSet _dataSet;
        private readonly Hashtable _disabledItems;

        public DisablingManager(DataSet dataSet)
        {
            this._dataSet = dataSet;
            this._disabledItems = new Hashtable();
        }

        public void AddEntity(DisablingEntity dEntity)
        {
            ArrayList list = new ArrayList();
            foreach (DataItem item in this._dataSet.DataItems)
            {
                if (dEntity.Filters(item))
                {
                    list.Add(item);
                    item.attachDisablingEntity(dEntity);
                }
            }
            this._disabledItems.Add(dEntity, list.ToArray(typeof(DataItem)));
        }

        public void DematerializeAllEntities()
        {
            DisablingEntity[] entityArray = new DisablingEntity[this._disabledItems.Keys.Count];
            int num = 0;
            foreach (DisablingEntity entity in this._disabledItems.Keys)
            {
                entityArray[num++] = entity;
            }
            foreach (DisablingEntity entity2 in entityArray)
            {
                entity2.Dematerialize();
            }
        }

        public void DematerializeUnselectedEntities()
        {
            ArrayList list = new ArrayList();
            foreach (DisablingEntity entity in this._disabledItems.Keys)
            {
                if (entity is UnselectedEntity)
                {
                    list.Add(entity);
                }
            }
            foreach (DisablingEntity entity2 in list)
            {
                entity2.Dematerialize();
            }
        }

        public void RemoveEntity(DisablingEntity dEntity)
        {
            DataItem[] itemArray = (DataItem[]) this._disabledItems[dEntity];
            foreach (DataItem item in itemArray)
            {
                item.detachDisablingEntity(dEntity);
            }
            this._disabledItems.Remove(dEntity);
        }
    }
}

