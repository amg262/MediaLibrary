using System;
using System.IO;
using NLog;
using NLog.Web;

namespace MediaLibrary
{
    internal class Program
    {
        // create static instance of Logger
        private static readonly Logger logger = NLogBuilder
            .ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            logger.Info("Program started");


            Console.WriteLine("(M)ovie  |  (A)lbum  |  (B)ook");
            string mediaType = Console.ReadLine();

            try
            {
                if (mediaType.ToUpper().Equals("M"))
                {
                    logger.Info("Media type of: Movie");

                    Console.Write("Media ID: ");
                    ulong.TryParse(Console.ReadLine(), out ulong mediaId);

                    Console.Write("Title: ");
                    string title = Console.ReadLine();

                    Console.Write("Director: ");
                    string director = Console.ReadLine();

                    Console.Write("Timespan (hr:min:sec) : ");
                    string timespanRaw = Console.ReadLine();

                    string[] timespanArr = timespanRaw.Split(":");
                    var timespanFormat = new TimeSpan(Convert.ToInt32(timespanArr[0]),
                        Convert.ToInt32(timespanArr[1]),
                        Convert.ToInt32(timespanArr[2]));
                    
                    Console.Write("Genres: ");
                    string genres = Console.ReadLine();
                }
                else if (mediaType.ToUpper().Equals("A"))
                {

                    logger.Info("Media type of: Album");

                    Console.Write("Media ID: ");
                    ulong.TryParse(Console.ReadLine(), out ulong mediaId);

                    Console.Write("Title: ");
                    string title = Console.ReadLine();

                    Console.Write("Artist: ");
                    string artist = Console.ReadLine();

                    Console.Write("Label: ");
                    string label = Console.ReadLine();

                    Console.Write("Genres: ");
                    string genres = Console.ReadLine();
                    
                    
                }
                else if (mediaType.ToUpper().Equals("B"))
                {
                    logger.Info("Media type of: Book");

                    Console.Write("Media ID: ");
                    ulong.TryParse(Console.ReadLine(), out ulong mediaId);

                    Console.Write("Title: ");
                    string title = Console.ReadLine();

                    Console.Write("Author: ");
                    string author = Console.ReadLine();

                    Console.Write("Pages: ");
                    UInt32.TryParse(Console.ReadLine(), out UInt32 pages);

                    Console.Write("Publisher: ");
                    string publisher = Console.ReadLine();
                    
                    Console.Write("Genres: ");
                    string genres = Console.ReadLine();
                    
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                Console.WriteLine(e);
                throw;
            }

            var movie = new Movie
            {
                mediaId = 123,
                title = "Greatest Movie Ever, The (2020)",
                director = "Jeff Grissom",
                // timespan (hours, minutes, seconds)
                runningTime = new TimeSpan(2, 21, 23),
                genres = {"Comedy", "Romance"}
            };

            Console.WriteLine(movie.Display());

            var album = new Album
            {
                mediaId = 321,
                title = "Greatest Album Ever, The (2020)",
                artist = "Jeff's Awesome Band",
                recordLabel = "Universal Music Group",
                genres = {"Rock"}
            };
            Console.WriteLine(album.Display());

            var book = new Book
            {
                mediaId = 111,
                title = "Super Cool Book",
                author = "Jeff Grissom",
                pageCount = 101,
                publisher = "",
                genres = {"Suspense", "Mystery"}
            };
            Console.WriteLine(book.Display());

            var scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            logger.Info(scrubbedFile);

            logger.Info("Program ended");
        }
    }
}