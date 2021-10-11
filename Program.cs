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