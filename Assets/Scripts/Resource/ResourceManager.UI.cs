namespace Gehenna
{
    public partial class ResourceManager
    {
        public bool TryGetUI<TModel>(string key, out UIBundle result) where TModel : BaseUIModel
        {
            result = null;
            if (!TryGetCatalog<UICatalog>(out var catalog))
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(UICatalog)} not found");
                return false;
            }
            
            if (!catalog.TryGet(key, out var bundle))
            {
                GehennaLogger.Log(this, LogType.Error, $"{nameof(UIBundle)} not found: " + typeof(TModel));
                return false;
            }

            result = bundle;
            return true;
        }

    }
}