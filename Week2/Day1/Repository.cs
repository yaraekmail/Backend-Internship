



    
// The constraint ensures that Repository<T> works only with reference types (classes),
// because this repository is designed to store entity objects like Employee, Customer, and StoneType.
public class Repository<T> where T : class  {
        private List<T> _items = new List<T>();

        public void Add(T item)
        {
            _items.Add(item);
        }
        // public List<T> GetAll()
        // {
           
        //     return _items;
        //     }
public IReadOnlyList<T> GetAll()
{
    return _items.AsReadOnly();
}

     public List<T> Find(Func<T, bool> condition)
{
    List<T> test = new List<T>();

    foreach(var i in _items)
    {
        if(condition(i))
        {
            test.Add(i);
        }
    }

    return test;
}
    }
   


