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
}

class Program
{
    
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

            books.Add(new Book() {Author = author,
                                  Title = title,
                                  Genre = genre,
                                  Price = p,
                                  PublishDate = new DateTime(),
                                  Description = description});
        }

        return books;
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

        xmlDocument.DocumentElement.AppendChild(newElement);

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

            if (book.Author == author && book.Title == title && book.Genre == genre
                                      && book.Price == p && book.PublishDate == date && book.Description == description)
                node.ParentNode.RemoveChild(node);
        }
        
    }

    static void Main(string[] args)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(@"Books.xml");

        // Fetching all books from the library
        List<Book> books = GetAllBooks(@"Books.xml");

        // Adding a book to the library
        Book newBook = new Book();
        newBook.Author = "Knuth Donald";
        newBook.Genre = "Analysis of algorithms";
        newBook.Price = 5;
        newBook.Description = "5th edition";
        newBook.PublishDate = DateTime.Now;
        newBook.Title = "Algorithms: theoretical approach to analysis";

        AddNewBook(@"Books.xml", newBook);
        
        DeleteBook(@"Books.xml", newBook);

        Console.ReadKey();
    }
}