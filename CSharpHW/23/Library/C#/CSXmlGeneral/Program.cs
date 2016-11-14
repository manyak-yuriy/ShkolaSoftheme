using System;
using System.Diagnostics.Eventing.Reader;
using System.Xml;
using System.Collections.Generic;
using System.Globalization;

public class Book
{
    public string Author { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public decimal Price { get; set; }
    public DateTime PublishDate { get; set; }
    public string Description { get; set; }

    public Book(Book b)
    {
        Author = b.Author;
        Title = b.Title;
        Genre = b.Genre;
        Price = b.Price;
        PublishDate = b.PublishDate;
        Description = b.Description;
    }

    public Book()
    {
        
    }
}

class Program
{
    static Book XmlReadNext(XmlNodeReader xmlNodeReader)
    {
        Book newBook = new Book();

        string author = xmlNodeReader.ReadElementContentAsString();
        string title = xmlNodeReader.ReadElementContentAsString();
        string genre = xmlNodeReader.ReadElementContentAsString();
        string price = xmlNodeReader.ReadElementContentAsString();
        string publishDate = xmlNodeReader.ReadElementContentAsString();
        string description = xmlNodeReader.ReadElementContentAsString();

        DateTime date;
        DateTime.TryParseExact(publishDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
            out date);

        decimal p;
        Decimal.TryParse(price, out p);

        newBook.Author = author;
        newBook.Title = title;
        newBook.Genre = genre;
        newBook.Price = p;
        newBook.PublishDate = date;
        newBook.Description = description;

        return newBook;
    }

    static List<Book> GetAllBooks(string fileName)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(fileName);

        List<Book> books = new List<Book>();

        XmlNodeList xmlNodes = xmlDocument.GetElementsByTagName("book");

        foreach (XmlNode node in xmlNodes)
        {
            XmlNodeReader xmlNodeReader = new XmlNodeReader(node);

            xmlNodeReader.Read();
            xmlNodeReader.Read();

            Book newBook = XmlReadNext(xmlNodeReader);

            books.Add(newBook);
        }

        return books;
    }

    static Book GetBookById(string fileName, string id)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(fileName);

        List<Book> books = new List<Book>();

        XmlNodeList xmlNodes = xmlDocument.GetElementsByTagName("book");

        foreach (XmlNode node in xmlNodes)
        {
            XmlNodeReader xmlNodeReader = new XmlNodeReader(node);

            xmlNodeReader.Read();
            xmlNodeReader.Read();

            Book newBook = XmlReadNext(xmlNodeReader);

            if (node.Attributes?["id"].InnerText == id)
                return newBook;
        }

        return null;
    }

    static void AddNewBook(string fileName, Book book)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(fileName);

        XmlElement newElement = xmlDocument.CreateElement("book");

        XmlAttribute newAttribute = xmlDocument.CreateAttribute("id");
        newAttribute.Value = Guid.NewGuid().ToString();
        newElement.Attributes.Append(newAttribute);

        XmlElement authorElement = xmlDocument.CreateElement("author");
        authorElement.InnerText = book.Author;
        newElement.AppendChild(authorElement);

        XmlElement titleElement = xmlDocument.CreateElement("title");
        titleElement.InnerText = book.Title;
        newElement.AppendChild(titleElement);

        XmlElement genreElement = xmlDocument.CreateElement("genre");
        genreElement.InnerText = book.Genre;
        newElement.AppendChild(genreElement);

        XmlElement priceElement = xmlDocument.CreateElement("price");
        priceElement.InnerText = book.Price.ToString(CultureInfo.CurrentCulture);
        newElement.AppendChild(priceElement);

        XmlElement publishDateElement = xmlDocument.CreateElement("publish_date");
        publishDateElement.InnerText = book.PublishDate.ToString(CultureInfo.CurrentCulture);
        newElement.AppendChild(publishDateElement);

        XmlElement descriptionElement = xmlDocument.CreateElement("description");
        descriptionElement.InnerText = book.Description;
        newElement.AppendChild(descriptionElement);

        xmlDocument.DocumentElement?.AppendChild(newElement);

        xmlDocument.Save(fileName);
    }

    static void DeleteBook(string fileName, Book book)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(fileName);

        List<Book> books = new List<Book>();

        XmlNodeList xmlNodes = xmlDocument.GetElementsByTagName("book");

        foreach (XmlNode node in xmlNodes)
        {
            XmlNodeReader xmlNodeReader = new XmlNodeReader(node);
            xmlNodeReader.Read();
            xmlNodeReader.Read();

            Book newBook = XmlReadNext(xmlNodeReader);

            if (book.Author == newBook.Author && book.Title == newBook.Title 
                                      && book.Genre == newBook.Genre
                                      && book.Price == newBook.Price 
                                      && book.PublishDate == newBook.PublishDate 
                                      && book.Description == newBook.Description)
                node.ParentNode?.RemoveChild(node);
        }
        
    }

    static void Main(string[] args)
    {
        const string fileName = @"Books.xml";

        // Fetching all books from the library
        List<Book> books = GetAllBooks(fileName);

        // Adding a book to the library
        Book newBook = new Book();
        newBook.Author = "Knuth Donald";
        newBook.Genre = "Analysis of algorithms";
        newBook.Price = 5;
        newBook.Description = "5th edition";
        newBook.PublishDate = DateTime.Now;
        newBook.Title = "Algorithms: theoretical approach to analysis";

        AddNewBook(fileName, newBook);
        
        // Deleting the book
        DeleteBook(fileName, newBook);

        // Search by id
        Book book = GetBookById(fileName, "bk101");

        Console.ReadKey();
    }
}