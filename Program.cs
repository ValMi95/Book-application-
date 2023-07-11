using static System.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Net.Sockets;
using System.ComponentModel;

namespace BookStoreApplication
{
    //The FIRST PART requires you to create a class template.
    class Book
    {
        //DATAFIELDS

        public static string[] categoryCodes = { "CS", "IS", "SE", "SO", "MI" };
        public static string[] categoryNames = { "Computer Science", "Information System", "Security", "Society", "Miscellaneous" };

        private string bookId;
        private string categoryNameOfBook;


        //PROPERTIES 
        private string bookTitle;
        private int numOfPages;
        private double price;

        public string BookTitle { get; set; }
        public int NumOfPages { get; set; }
        public double Price { get; set; }


        public string BookId
        {
            get
            {
                return bookId;
            }
            set
            {
                //string code = BookId.Substring(0, 2);
                //if (code != "CS" && code != "IS" && code != "SE" && code != "SO" && code != "MI")
                //    bookId = "MI";
                //else
                //    bookId = value;

                bookId = value;
            }
        }

        //Category property is a read-only property that is assigned a value when the book id is set.
        public string CategoryNameOfBook
        {
            get
            {
                return categoryNameOfBook;
            }
        }

        //CONSTRUCTORS
        //one with no parameter: public Book()
        public Book()
        {
        }

        //one with parameter for all data fields:
        public Book(string abookTitle, string abookId, int anumPages, double aprice)
        {
            BookId = abookId;
            BookTitle = abookTitle;
            NumOfPages = anumPages;
            Price = aprice;
        }


        //METHOD
        //A ToString method to return information of a book object using the format given in the screen shot under: Information of all Books
        public override string ToString()
        {
            return "*" + BookId + " " + BookTitle + " " + NumOfPages + " " + Price.ToString("C");
        }

    }


    //The SECOND PART requires you to create an Application class which will use the class template and
    //perform a number of specified tasks.
    class Program//Template
    {
        //MAIN METHOD which will only contain calls to the other methods and the necessary variable declarations
        static void Main(string[] args)
        {

            //1. First, call the method InputValue to prompt the user for the number of books that is between 1 and 30 (inclusive).
            int num = InputValue();
            Book[] books = new Book[num];

            //2. Then call method GetBookData create an array of books. //MAYBE CHECK pag 377
            GetBookData(num, books);

            //3. Then call method DisplayAllBooks to display all books in the array. //MAYBE CHECK pag 388
            DisplayAllBooks(books);


            //4. Then call method GetBookLists to allow the user to input a category code and see the information of the category.
            GetBookLists(books);
        }

        //1. A Method to input an integer number that is between (inclusive) the range of a lower bound and an upper bound.
        public static int InputValue(int min = 1, int max = 30) 
        {
            int num;
            string numEntry;
            Console.WriteLine("Please enter a number which is the range of {0} and {1} >>>  ", min, max);
            numEntry = ReadLine();
            num = Convert.ToInt32(numEntry);
            WriteLine("---------------------------------------------------");
            while (num < min || num > max)
            {
                Write("{0} is invalid number, please try again >>> ", num);
                numEntry = ReadLine();
                num = Convert.ToInt32(numEntry);
            }
            return num;



        }
        //2. A Method to check if an input string satisfies the following conditions:
        public static bool IsValid(string id)
        {

            if (id.Length != 5)
                return false;

            if (!(Char.IsUpper(id, 0) || Char.IsUpper(id, 1)))
                return false;

            if (!(Char.IsDigit(id, 2) || Char.IsDigit(id, 3) || Char.IsDigit(id, 4)))
                return false;

            return true;
        }
        ///DisplayCategories
        public static void DisplayCategories()
        {

            WriteLine("Categories are: ");

            for (int i = 0; i < Book.categoryCodes.Length; i++)
            {
                string catCodes = Book.categoryCodes[i];
                string catNames = Book.categoryNames[i];
                Console.WriteLine($"{catCodes}\t{catNames} ");
            }
        }
        //3. A Method, to fill an array of books. 
        private static void GetBookData(int num, Book[] books)
        {

            string inputName;
            string inputId;
            string inputPrice;
            string inputPages;
            int price, pages;

            //WriteLine("-------------------------------------------------");

            for (int v = 0; v < num; v++)
            {
                DisplayCategories();
                WriteLine("");

                //Prompting name
                Write("* Please enter the name of a book >>>> ");
                inputName = ReadLine();

                WriteLine("");
                //Prompting book id and call method IsValid //If not valid, prompt to re-enter //MAYBE! pag 284
                Write("* Please enter a book id which starts with a category code and ends with a 3 digit number >>>  ");
                inputId = ReadLine();
                while (!IsValid(inputId))
                {
                    Write("- Sorry, id not found. Please try again >>> ");
                    inputId = ReadLine();
                }
                WriteLine("");
                //Prompting price
                Write("* Please enter the price >>>> ");
                inputPrice = ReadLine();
                int.TryParse(inputPrice, out price);
                WriteLine("");
                //Prompting pages
                Write("* Please enter the number of pages >>>> ");
                inputPages = ReadLine();
                int.TryParse(inputPages, out pages);

                WriteLine("");

                Book book = new Book(inputName, inputId, price, pages);
                books[v] = book;

            }

        }
        //4. After the data entry is complete, write a Method to display information of all books that have been entered.
        public static void DisplayAllBooks(Book[] books)
        {

            WriteLine("");
            WriteLine("-------------------------------------------------------------");
            Write("Information of all books: ");
            WriteLine("");
            WriteLine("");
            for (int x = 0; x < books.Length; x++)
                WriteLine("Book #{0}:       {1}", x, books[x].ToString());

            WriteLine("");
            WriteLine("-------------------------------------------------------------");
            WriteLine("");

        }

        //5. After the data entry is complete, write a Method to display the valid book categories,
        private static void GetBookLists(Book[] books) 
        {

            DisplayCategories();
            WriteLine("");
            WriteLine("");

            const char quit = 'Z';
            const char yes = 'Y';
            string inputCode = " ";
            char respons;

            Write("Press 'Y' to see the books by category or press 'Z' to quit >> ");
            inputCode = ReadLine();
            respons = Convert.ToChar(inputCode);
            WriteLine("");


            while (respons == 'Y')
            {
                WriteLine("");
                Write("Plese enter a category code >>> ");
                WriteLine("");
                inputCode = ReadLine();

                while (inputCode != "CS" && inputCode != "IS" && inputCode != "SE" && inputCode != "SO" && inputCode != "MI")
                {
                    Write("Invalid code, please enter a valid code >>> ");
                    inputCode = ReadLine();

                }

                bool found = false;

                for (int i = 0; i < books.Length; i++)
                {
                    string substring = books[i].BookId.Substring(0, 2);
                    if (inputCode == substring)
                    {
                        found = true;
                        Write("The books of the category {0} are: ", inputCode);
                        WriteLine("");
                        Write("{0}\t{1}\t${2}\t{3}", books[i].BookId, books[i].BookTitle, books[i].Price, books[i].NumOfPages);
                        WriteLine();
                    }
                    //else
                        //Write("No books available in this category");
                }

                if (!found)
                {
                    Write("No books in this category. Press 'Y' to see the books by category or press 'Z' to quit >> ");
                    WriteLine("");
                    inputCode = ReadLine();
                    respons = Convert.ToChar(inputCode);
                    //if(respons == 'Z')
                    //Write("Thank you!! ");
                }
                else
                {
                    if (respons == 'Z')
                        Write("Thank you!! ");

                }
            }

            Write("Thank you!! ");
        }

    }
}





