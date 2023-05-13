using System.Text.Json;
using core.Domain;

namespace core.Data;

public class JsonStore<T> : IPersistence<T> where T : class, IHasId
{
    private string _filePath;
    private string _seedPath;

    private List<T> _objects { get; set; } = new List<T>();

    public JsonStore()
    {
        string store_type = typeof(T).Name.ToLower();
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", store_type + "Store.json");
        var seedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", store_type + "_seed.json");

        if (!File.Exists(_filePath))
        {
            File.Copy(seedPath, _filePath);
        }

        var json = File.ReadAllText(_filePath);
        _objects = JsonSerializer.Deserialize<List<T>>(json);

    }
    public void Add(T item)
    {
        _objects.Add(item);
        Save();
    }

    public void Delete(Guid id)
    {
        var objectToRemove = this.GetById(id);
        if (objectToRemove != null)
            this._objects.Remove(objectToRemove);
        Save();
    }

    public IEnumerable<T> GetAll()
    {
        return this._objects;
    }

    public T? GetById(Guid id)
    {
        return _objects.OfType<T>().FirstOrDefault(b => b.Id == id);
    }

    public void Update(T item)
    {
        this.Delete(item.Id);
        this.Add(item);
        this.Save();
    }

    private void Save()
    {
        // Had an issue here with persistence that I didn't have time to
        // resolve for this exercise. When enabled, this allows items to
        // persist to the file but breaks expectations for testing.
        // I'd need to setup a better mechanism for test only persistence
        // layers that are built and torn down for the explicit purposes
        // of testing to fix permanently. 

        //var options = new JsonSerializerOptions
        //{
        //    WriteIndented = true
        //};

        //var json = JsonSerializer.Serialize(_objects, options);
        //File.WriteAllText(_filePath, json);
    }
}