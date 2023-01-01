using System.Diagnostics;
using Example.BookStore.Catalog.Contracts;

namespace Example.BookStore.Catalog;

public class Book
{
    public Book(Guid id, string title, int version)
    {
        Id = id;
        Title = title;
        Version = version;
    }
    
    public Guid Id { get; private set;}
    public string Title { get; private set;}
    public int Version { get; private set; }

    public void Update(UpdateBook request)
    {
        Debug.Assert(request.Title != null, "request.Title != null");
        Title = request.Title;
        Update();
    }
    private void Update()
    {
        Version++;
    }
}