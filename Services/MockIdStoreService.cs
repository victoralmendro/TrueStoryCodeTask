namespace TrueStoryCodeTask.Services
{
    public class MockIdStoreService
    {
        private readonly HashSet<string> _ids = new();
        private readonly object _lock = new();

        public void Add(string id)
        {
            lock (_lock)
            {
                _ids.Add(id);
            }
        }

        public void Remove(string id)
        {
            lock (_lock)
            {
                _ids.Remove(id);
            }
        }

        public IReadOnlyCollection<string> GetAll()
        {
            lock (_lock)
            {
                return _ids.ToList().AsReadOnly();
            }
        }

        public bool Contains(string id)
        {
            lock (_lock)
            {
                return _ids.Contains(id);
            }
        }
    }

}
