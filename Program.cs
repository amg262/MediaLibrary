using System;
using System.Collections.Generic;
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


            Console.WriteLine("(M)ovie  |  (A)lbum  |  (B)ook  |  (S)earch");
            var mediaType = Console.ReadLine();

            try
            {
                if (mediaType != null && mediaType.ToUpper().Equals("M"))
                {
                    logger.Info("Media type of: Movie");

                    Console.Write("Media ID: ");
                    ulong.TryParse(Console.ReadLine(), out var mediaId);

                    Console.Write("Title: ");
                    var title = Console.ReadLine();

                    Console.Write("Director: ");
                    var director = Console.ReadLine();

                    Console.Write("Timespan (hr:min:sec) : ");
                    var timespanRaw = Console.ReadLine();

                    var timespanArr = timespanRaw.Split(":");
                    var timespanFormat = new TimeSpan(Convert.ToInt32(timespanArr[0]),
                        Convert.ToInt32(timespanArr[1]),
                        Convert.ToInt32(timespanArr[2]));

                    Console.Write("Genres (Genre1,Genre2): ");
                    var genresRaw = Console.ReadLine();
                    var genresArr = new List<string>(genresRaw.Split(","));


                    var movie = new Movie
                    {
                        mediaId = mediaId,
                        title = title,
                        director = director,
                        runningTime = timespanFormat,
                        genres = genresArr
                    };
                }
                else if (mediaType != null && mediaType.ToUpper().Equals("A"))
                {
                    logger.Info("Media type of: Album");

                    Console.Write("Media ID: ");
                    ulong.TryParse(Console.ReadLine(), out var mediaId);

                    Console.Write("Title: ");
                    var title = Console.ReadLine();

                    Console.Write("Artist: ");
                    var artist = Console.ReadLine();

                    Console.Write("Label: ");
                    var label = Console.ReadLine();

                    Console.Write("Genres: ");
                    var genres = new List<string> {Console.ReadLine()};

                    var album = new Album
                    {
                        mediaId = mediaId,
                        title = title,
                        artist = artist,
                        recordLabel = label,
                        genres = genres
                    };
                }
                else if (mediaType != null && mediaType.ToUpper().Equals("B"))
                {
                    logger.Info("Media type of: Book");

                    Console.Write("Media ID: ");
                    ulong.TryParse(Console.ReadLine(), out var mediaId);

                    Console.Write("Title: ");
                    var title = Console.ReadLine();

                    Console.Write("Author: ");
                    var author = Console.ReadLine();

                    Console.Write("Pages: ");
                    ushort.TryParse(Console.ReadLine(), out var pages);

                    Console.Write("Publisher: ");
                    var publisher = Console.ReadLine();

                    Console.Write("Genres: ");
                    var genres = new List<string> {Console.ReadLine()};

                    var book = new Book
                    {
                        mediaId = mediaId,
                        title = title,
                        author = author,
                        pageCount = pages,
                        publisher = publisher,
                        genres = genres
                    };
                    Console.WriteLine(book.Display());
                }
                else if (mediaType.ToUpper().Equals("S"))
                {
                    Console.WriteLine("Search by Keyword: ");
                    var keyword = Console.ReadLine();
                    var m = new Movie();

                    var file = new MovieFile("movies.scrubbed.csv");

                    file.Search(keyword);
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                Console.WriteLine(e);
                throw;
            }

            var scrubbedMovieFile = MovieFileScrubber.ScrubMovies("movies.csv");
            var scrubbedBookFile = BookFileScrubber.ScrubMovies("books.csv");
            var scrubbedAlbumFile = AlbumFileScrubber.ScrubMovies("albums.csv");


            logger.Info(scrubbedMovieFile);

            logger.Info("Program ended");
        }
    }
}